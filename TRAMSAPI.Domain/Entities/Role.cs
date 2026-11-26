namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores role definitions used for role-based access control per tenant.
/// </summary>
public class Role
{
    public long RoleId { get; set; }
    public long TenantId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual TenantMaster? Tenant { get; set; }
    public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
    public virtual ICollection<AccessPermissionMapping> AccessPermissionMappings { get; set; } = new List<AccessPermissionMapping>();
}
