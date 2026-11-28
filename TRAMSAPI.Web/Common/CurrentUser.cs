using System.Security.Claims;
using TRAMSAPI.Application.Interfaces;

namespace TRAMSAPI.Web.Common;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

    public long? UserId
    {
        get
        {
            var idClaim = User?.FindFirst("user_id")?.Value
                          ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return long.TryParse(idClaim, out var id) ? id : null;
        }
    }

    public long? TenantId
    {
        get
        {
            var tenantClaim = User?.FindFirst("tenant_id")?.Value;
            return long.TryParse(tenantClaim, out var id) ? id : null;
        }
    }

    public string? Email =>
        User?.FindFirst(ClaimTypes.Email)?.Value
        ?? User?.FindFirst("email")?.Value;

    public IReadOnlyList<string> Roles =>
        User?.FindAll(ClaimTypes.Role).Select(c => c.Value).Distinct().ToList()
        ?? new List<string>();
}
