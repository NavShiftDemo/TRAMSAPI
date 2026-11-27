using System.ComponentModel.DataAnnotations;

namespace TRAMSAPI.Application.DTOs;

public class TenantRegistrationRequestDto
{
    // Page 1 - Company Details

    [Required]
    [StringLength(150)]
    public string BusinessName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string CompanyEmail { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string CompanyPhoneNo { get; set; } = string.Empty;


    // Page 2 - First Owner Details

    [Required]
    [StringLength(150)]
    public string OwnerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string OwnerEmail { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string OwnerPhoneNo { get; set; } = string.Empty;

    [StringLength(20)]
    public string? AlternatePhoneNo { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
