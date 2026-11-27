using System.ComponentModel.DataAnnotations;

namespace TRAMSAPI.Application.DTOs.Auth;

public class ForgotPasswordRequestDto
{
    [Required(ErrorMessage = "Email or phone number is required.")]
    public string Identifier { get; set; } = string.Empty;
}
