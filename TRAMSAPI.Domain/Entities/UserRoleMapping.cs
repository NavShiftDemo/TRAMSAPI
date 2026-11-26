namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Connects users to one or more roles within their own tenant.
/// </summary>
public class UserRoleMapping
{
    public long UserRoleId { get; set; }
    public long TenantId { get; set; }
    public long UserId { get; set; }
    public long RoleId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual TenantMaster? Tenant { get; set; }
    public virtual User? User { get; set; }
    public virtual Role? Role { get; set; }
}
