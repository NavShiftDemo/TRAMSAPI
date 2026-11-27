namespace TRAMSAPI.Application.DTOs.Auth;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string TokenType { get; set; } = "Bearer";
    public DateTime ExpiresAt { get; set; }
    public long UserId { get; set; }
    public long TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PhoneNo { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
}
