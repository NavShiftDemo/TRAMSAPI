using System.Net;
using System.Text.Json;
using TRAMSAPI.Application.Interfaces;

namespace TRAMSAPI.Web.Middleware;

/// <summary>
/// Ensures that authenticated requests belong to active users and active tenants in real time.
/// Prevents deactivated users from using non-expired JWT tokens.
/// </summary>
public class ActiveAccountValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ActiveAccountValidationMiddleware> _logger;

    public ActiveAccountValidationMiddleware(
        RequestDelegate next,
        ILogger<ActiveAccountValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ICurrentUser currentUser,
        IAuthRepository authRepository)
    {
        if (currentUser.IsAuthenticated && currentUser.UserId.HasValue)
        {
            var user = await authRepository.GetUserByIdAsync(currentUser.UserId.Value);

            if (user == null || !user.IsActive || (user.Tenant != null && !user.Tenant.IsActive))
            {
                _logger.LogWarning(">>> [ACCOUNT REJECTED] Access revoked for User {UserId}. Active: {IsActive}, TenantActive: {TenantActive}",
                    currentUser.UserId.Value, user?.IsActive, user?.Tenant?.IsActive);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var response = new
                {
                    statusCode = 401,
                    error = "AccountDeactivated",
                    message = "Your account or organization is inactive. Please contact your administrator."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }
        }

        await _next(context);
    }
}
