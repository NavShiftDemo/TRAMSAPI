using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Services;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, DateTime ExpiresAt) GenerateToken(User user, IEnumerable<string> roles)
    {
        var secret = _configuration["JwtSettings:Secret"] ?? "TRAMS_SUPER_SECRET_JWT_SIGNING_KEY_2026_MIN_32_CHARS!";
        var issuer = _configuration["JwtSettings:Issuer"] ?? "TRAMSAPI";
        var audience = _configuration["JwtSettings:Audience"] ?? "TRAMSAPI.Clients";
        var expiryMinutes = int.TryParse(_configuration["JwtSettings:TokenExpiryInMinutes"], out var mins) ? mins : 120;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.Name),
            new("phone_number", user.PhoneNo),
            new("tenant_id", user.TenantId.ToString()),
            new("is_system_user", user.IsSystemUser.ToString().ToLower()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
        }

        if (user.Tenant != null && !string.IsNullOrWhiteSpace(user.Tenant.TenantCode))
        {
            claims.Add(new Claim("tenant_code", user.Tenant.TenantCode));
        }

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (tokenHandler.WriteToken(token), expiresAt);
    }
}
