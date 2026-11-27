using System.ComponentModel.DataAnnotations;
using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Application.DTOs.Auth;

public class VerifyOtpRequestDto
{
    [Required(ErrorMessage = "Email or phone number is required.")]
    public string Identifier { get; set; } = string.Empty;

    [Required(ErrorMessage = "OTP code is required.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "OTP must be 6 digits.")]
    public string OtpCode { get; set; } = string.Empty;

    public Purpose Purpose { get; set; } = Purpose.RESET_PASSWORD;
}
