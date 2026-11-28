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

        // Minimal, high-performance claims payload
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new("user_id", user.UserId.ToString()),
            new("tenant_id", user.TenantId.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var createdAt = DateTime.UtcNow;
        var expiresAt = createdAt.AddMinutes(expiryMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            IssuedAt = createdAt,
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
