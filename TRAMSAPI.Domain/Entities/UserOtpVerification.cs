using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores verification codes / OTPs for account verification and password reset.
/// </summary>
public class UserOtpVerification
{
    public long OtpId { get; set; }
    public long UserId { get; set; }
    public string OtpHash { get; set; } = string.Empty;
    public Purpose Purpose { get; set; }
    public bool IsVerified { get; set; } = false;
    public int AttemptCount { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    public virtual User? User { get; set; }
}
