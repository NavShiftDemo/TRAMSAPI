using System.Security.Cryptography;
using System.Text;
using TRAMSAPI.Application.DTOs.Auth;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Domain.Entities;
using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IOtpNotificationService _notificationService;

    // Secret salt used for HMAC verification of reset tokens
    private const string ResetTokenSecret = "TRAMS_RESET_PWD_SECRET_SALT_2026";
    private const int OtpValidityMinutes = 5;
    private const int MaxOtpAttempts = 3;
    private const int OtpCooldownSeconds = 120; // 2-minute cooldown between OTP requests

    public AuthService(
        IAuthRepository authRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IOtpNotificationService notificationService)
    {
        _authRepository = authRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _notificationService = notificationService;
    }

    /// <summary>
    /// Authenticates a user by email, checks BCrypt hash, logs activity, and generates JWT.
    /// </summary>
    public async Task<LoginResponseDto?> LoginAsync(
        LoginRequestDto request,
        string? ipAddress = null,
        string? deviceInfo = null)
    {
        var email = request.Email.Trim().ToLower();
        var user = await _authRepository.GetUserByEmailAsync(email);

        // 1. Check if user exists and is active
        if (user == null || !user.IsActive)
        {
            await RecordHistoryAsync(user?.TenantId, user?.UserId, EventType.FAILED_LOGIN, ipAddress, deviceInfo);
            return null;
        }

        // 2. Check if user's tenant is active
        if (user.Tenant != null && !user.Tenant.IsActive)
        {
            await RecordHistoryAsync(user.TenantId, user.UserId, EventType.FAILED_LOGIN, ipAddress, deviceInfo);
            return null;
        }

        // 3. Verify password hash using BCrypt
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            await RecordHistoryAsync(user.TenantId, user.UserId, EventType.FAILED_LOGIN, ipAddress, deviceInfo);
            return null;
        }

        // 4. Extract assigned roles
        var roles = user.UserRoleMappings
            .Where(urm => urm.IsActive && urm.Role != null && urm.Role.IsActive)
            .Select(urm => urm.Role!.RoleName)
            .Distinct()
            .ToList();

        // 5. Generate signed JWT token
        var (token, expiresAt) = _jwtTokenGenerator.GenerateToken(user, roles);

        // 6. Record successful login audit
        await RecordHistoryAsync(user.TenantId, user.UserId, EventType.LOGIN, ipAddress, deviceInfo);

        return new LoginResponseDto
        {
            Token = token,
            TokenType = "Bearer",
            ExpiresAt = expiresAt,
            UserId = user.UserId,
            TenantId = user.TenantId,
            Name = user.Name,
            Email = user.Email,
            PhoneNo = user.PhoneNo,
            Roles = roles
        };
    }

    /// <summary>
    /// Generates a secure 6-digit OTP, saves the hash, and dispatches via notification service.
    /// </summary>
    public async Task<(bool Success, string Message)> ForgotPasswordAsync(ForgotPasswordRequestDto request)
    {
        var email = request.Email.Trim().ToLower();
        var user = await _authRepository.GetUserByEmailAsync(email);

        // Security best practice: don't reveal whether user exists
        if (user == null)
        {
            return (true, "If an account exists with this email, an OTP has been sent.");
        }

        if (!user.IsActive)
        {
            return (false, "Account is inactive. Please contact your administrator.");
        }

        // Check 2-minute cooldown timer between OTP requests
        var recentOtp = await _authRepository.GetLatestOtpAsync(user.UserId, Purpose.RESET_PASSWORD);
        if (recentOtp != null)
        {
            var timeSinceCreation = DateTime.UtcNow - recentOtp.CreatedAt;
            if (timeSinceCreation < TimeSpan.FromSeconds(OtpCooldownSeconds))
            {
                var secondsRemaining = Math.Max(1, (int)(TimeSpan.FromSeconds(OtpCooldownSeconds) - timeSinceCreation).TotalSeconds);

                if (recentOtp.AttemptCount >= MaxOtpAttempts)
                {
                    return (false, $"Too many failed attempts. Please wait {secondsRemaining} second(s) before requesting a new OTP.");
                }

                if (!recentOtp.IsVerified)
                {
                    return (false, $"An OTP was recently sent. Please wait {secondsRemaining} second(s) before requesting a new code.");
                }
            }
        }

        // Generate 6-digit cryptographic random OTP code
        var otpCode = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

        // Hash the OTP with BCrypt before storing in database
        var otpHash = BCrypt.Net.BCrypt.HashPassword(otpCode);

        var otpEntity = new UserOtpVerification
        {
            UserId = user.UserId,
            OtpHash = otpHash,
            Purpose = Purpose.RESET_PASSWORD,
            IsVerified = false,
            AttemptCount = 0,
            CreatedAt = DateTime.UtcNow
        };

        await _authRepository.SaveOtpAsync(otpEntity);

        // Send OTP via Email / SMS
        var destination = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : user.PhoneNo;
        await _notificationService.SendOtpAsync(destination, otpCode, Purpose.RESET_PASSWORD);

        var maskedEmail = MaskEmail(destination);
        return (true, $"OTP has been sent successfully to {maskedEmail}.");
    }

    /// <summary>
    /// Verifies the 6-digit OTP code, checks expiration and attempt limits, and produces a ResetToken.
    /// </summary>
    public async Task<(bool Success, string Message, string? ResetToken)> VerifyOtpAsync(VerifyOtpRequestDto request)
    {
        var email = request.Email.Trim().ToLower();
        var user = await _authRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            return (false, "Invalid request. Please try again.", null);
        }

        var latestOtp = await _authRepository.GetLatestOtpAsync(user.UserId, request.Purpose);
        if (latestOtp == null)
        {
            return (false, "No active OTP request found. Please request a new OTP.", null);
        }

        if (latestOtp.IsVerified)
        {
            return (false, "This OTP has already been verified.", null);
        }

        // Brute-force protection: reject if attempt count exceeded
        if (latestOtp.AttemptCount >= MaxOtpAttempts)
        {
            return (false, "Too many failed attempts. This OTP has been invalidated. Please request a new one.", null);
        }

        // Expiration check
        if (DateTime.UtcNow - latestOtp.CreatedAt > TimeSpan.FromMinutes(OtpValidityMinutes))
        {
            return (false, "OTP has expired. Please request a new OTP.", null);
        }

        // Verify the code with BCrypt
        bool isCodeValid = BCrypt.Net.BCrypt.Verify(request.OtpCode, latestOtp.OtpHash);
        if (!isCodeValid)
        {
            latestOtp.AttemptCount++;
            await _authRepository.UpdateOtpAsync(latestOtp);

            int remaining = MaxOtpAttempts - latestOtp.AttemptCount;
            return (false, $"Invalid OTP. {remaining} attempt(s) remaining.", null);
        }

        // Mark OTP as verified
        latestOtp.IsVerified = true;
        await _authRepository.UpdateOtpAsync(latestOtp);

        // Generate signed, tamper-evident ResetToken
        var resetToken = GenerateResetToken(user.UserId, latestOtp.OtpId);

        return (true, "OTP verified successfully.", resetToken);
    }

    /// <summary>
    /// Resets the user's password if the ResetToken is valid and meets requirements.
    /// </summary>
    public async Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        var email = request.Email.Trim().ToLower();
        var user = await _authRepository.GetUserByEmailAsync(email);

        if (user == null)
        {
            return (false, "Invalid request.");
        }

        // Validate and decode reset token
        var (isValidToken, userId, otpId) = ValidateResetToken(request.ResetToken);
        if (!isValidToken || userId != user.UserId)
        {
            return (false, "Invalid or tampered reset token.");
        }

        // Ensure the underlying OTP was indeed verified and not already consumed
        var latestOtp = await _authRepository.GetLatestOtpAsync(user.UserId, Purpose.RESET_PASSWORD);
        if (latestOtp == null || latestOtp.OtpId != otpId || !latestOtp.IsVerified || latestOtp.AttemptCount >= MaxOtpAttempts)
        {
            return (false, "Reset session has expired or already been used. Please request a new OTP.");
        }

        // Passwords match validation
        if (request.NewPassword != request.ConfirmPassword)
        {
            return (false, "New password and confirmation password do not match.");
        }

        // Hash new password using BCrypt
        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

        // Persist new password
        bool updated = await _authRepository.UpdatePasswordAsync(user.UserId, newPasswordHash);
        if (!updated)
        {
            return (false, "Failed to update password. Please try again.");
        }

        // Invalidate OTP to prevent replay (set to MaxOtpAttempts instead of magic number 99)
        latestOtp.AttemptCount = MaxOtpAttempts;
        await _authRepository.UpdateOtpAsync(latestOtp);

        return (true, "Password has been reset successfully. You can now log in with your new password.");
    }

    // --- Private Helpers ---

    private async Task RecordHistoryAsync(
        long? tenantId,
        long? userId,
        EventType eventType,
        string? ipAddress,
        string? deviceInfo)
    {
        var history = new UserLoginHistory
        {
            TenantId = tenantId,
            UserId = userId,
            EventType = eventType,
            EventTime = DateTime.UtcNow,
            IpAddress = ipAddress,
            DeviceInfo = deviceInfo
        };

        await _authRepository.RecordLoginHistoryAsync(history);
    }

    private static string GenerateResetToken(long userId, long otpId)
    {
        var payload = $"{userId}:{otpId}:{DateTime.UtcNow.Ticks}";
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ResetTokenSecret));
        var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(payload)));
        var rawToken = $"{payload}:{hash}";
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(rawToken));
    }

    private static (bool IsValid, long UserId, long OtpId) ValidateResetToken(string resetToken)
    {
        try
        {
            var rawBytes = Convert.FromBase64String(resetToken);
            var rawToken = Encoding.UTF8.GetString(rawBytes);
            var parts = rawToken.Split(':');

            if (parts.Length != 4) return (false, 0, 0);

            long userId = long.Parse(parts[0]);
            long otpId = long.Parse(parts[1]);
            long ticks = long.Parse(parts[2]);
            string providedHash = parts[3];

            // Verify HMAC signature
            var payload = $"{userId}:{otpId}:{ticks}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(ResetTokenSecret));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(payload)));

            if (computedHash != providedHash) return (false, 0, 0);

            // Token expires after 15 minutes
            var tokenTime = new DateTime(ticks, DateTimeKind.Utc);
            if (DateTime.UtcNow - tokenTime > TimeSpan.FromMinutes(15)) return (false, 0, 0);

            return (true, userId, otpId);
        }
        catch
        {
            return (false, 0, 0);
        }
    }

    private static string MaskEmail(string email)
    {
        if (email.Contains('@'))
        {
            var parts = email.Split('@');
            var name = parts[0];
            var domain = parts[1];
            var maskedName = name.Length <= 2
                ? name[0] + "*"
                : name[0] + new string('*', Math.Min(name.Length - 2, 6)) + name[^1];
            return $"{maskedName}@{domain}";
        }

        return email.Length <= 4
            ? "****"
            : new string('*', email.Length - 4) + email[^4..];
    }
}
