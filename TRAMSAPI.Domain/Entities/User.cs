namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores a person's account, contact details and authentication information.
/// </summary>
public class User
{
    public long UserId { get; set; }
    public long TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PhoneNo { get; set; } = string.Empty;
    public string? AlternatePhoneNo { get; set; }
    public string? Address { get; set; }
    public string? ProfileImageUrl { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsSystemUser { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }

    // Navigation properties
    public virtual TenantMaster? Tenant { get; set; }
    public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
    public virtual DriverOperatorDetail? DriverOperatorDetail { get; set; }
    public virtual ICollection<UserLoginHistory> LoginHistories { get; set; } = new List<UserLoginHistory>();
    public virtual ICollection<UserOtpVerification> OtpVerifications { get; set; } = new List<UserOtpVerification>();
    public virtual ICollection<AccessPermissionMapping> AccessPermissionMappings { get; set; } = new List<AccessPermissionMapping>();
    public virtual ICollection<AccountNumber> AccountNumbers { get; set; } = new List<AccountNumber>();
    public virtual ICollection<AmountTally> AmountTallies { get; set; } = new List<AmountTally>();
    public virtual ICollection<SalaryConfig> SalaryConfigs { get; set; } = new List<SalaryConfig>();
}
