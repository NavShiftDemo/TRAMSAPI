using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Audit trail for all login, logout, and failed login activity.
/// </summary>
public class UserLoginHistory
{
    public long LoginHistoryId { get; set; }
    public long? TenantId { get; set; }
    public long? UserId { get; set; }
    public EventType EventType { get; set; }
    public DateTime EventTime { get; set; } = DateTime.Now;
    public string? IpAddress { get; set; }
    public string? DeviceInfo { get; set; }

    // Navigation properties
    public virtual TenantMaster? Tenant { get; set; }
    public virtual User? User { get; set; }
}
