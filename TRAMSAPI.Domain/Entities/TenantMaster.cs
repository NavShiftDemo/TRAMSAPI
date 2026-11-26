namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores each registered company / business tenant.
/// </summary>
public class TenantMaster
{
    public long TenantId { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string TenantCode { get; set; } = string.Empty;
    public string? CompanyEmail { get; set; }
    public string? CompanyPhoneNo { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
    public virtual ICollection<DriverOperatorDetail> DriverOperatorDetails { get; set; } = new List<DriverOperatorDetail>();
    public virtual ICollection<AccessPermissionMapping> AccessPermissionMappings { get; set; } = new List<AccessPermissionMapping>();
}
