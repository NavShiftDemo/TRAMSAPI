namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Master table for implementations shared by vehicles.
/// </summary>
public class ImplementationMaster
{
    public long ImplementationMasterId { get; set; }
    public long TenantId { get; set; }
    public long? VehicleCategoryId { get; set; }
    public string ImplementationName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }
}
