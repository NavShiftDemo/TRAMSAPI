using System.ComponentModel.DataAnnotations;

namespace TRAMSAPI.Application.DTOs.Auth;

public class LoginRequestDto
{
    [Required(ErrorMessage = "Email or phone number is required.")]
    public string Identifier { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}
