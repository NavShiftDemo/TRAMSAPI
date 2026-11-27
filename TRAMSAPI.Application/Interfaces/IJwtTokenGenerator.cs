using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Application.Interfaces;

public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user, IEnumerable<string> roles);
}
