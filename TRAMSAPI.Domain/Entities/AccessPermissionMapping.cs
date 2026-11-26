namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores both default role permissions and individual user-specific additional permissions.
/// </summary>
public class AccessPermissionMapping
{
    public long AccessPermissionId { get; set; }
    public long TenantId { get; set; }
    public long? RoleId { get; set; }
    public long? UserId { get; set; }
    public long ActionId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual TenantMaster? Tenant { get; set; }
    public virtual Role? Role { get; set; }
    public virtual User? User { get; set; }
    public virtual ActionMaster? Action { get; set; }
}
