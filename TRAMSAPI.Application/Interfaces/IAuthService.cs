using TRAMSAPI.Application.DTOs.Auth;

namespace TRAMSAPI.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, string? ipAddress = null, string? deviceInfo = null);
    Task<(bool Success, string Message)> ForgotPasswordAsync(ForgotPasswordRequestDto request);
    Task<(bool Success, string Message, string? ResetToken)> VerifyOtpAsync(VerifyOtpRequestDto request);
    Task<(bool Success, string Message)> ResetPasswordAsync(ResetPasswordRequestDto request);
}
