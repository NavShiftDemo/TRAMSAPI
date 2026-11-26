namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores additional information required only for driver/operators.
/// </summary>
public class DriverOperatorDetail
{
    public long DriverOperatorId { get; set; }
    public long TenantId { get; set; }
    public long UserId { get; set; }
    public string? LicenseNo { get; set; }
    public DateOnly? LicenseExpiryDate { get; set; }
    public int? ExperienceYears { get; set; }
    public string? Specialization { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual TenantMaster? Tenant { get; set; }
    public virtual User? User { get; set; }
}
