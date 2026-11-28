namespace TRAMSAPI.Application.Interfaces;

/// <summary>
/// Provides strongly-typed access to the current authenticated user's context across the application.
/// </summary>
public interface ICurrentUser
{
    long? UserId { get; }
    long? TenantId { get; }
    string? Email { get; }
    IReadOnlyList<string> Roles { get; }
    bool IsAuthenticated { get; }
}
