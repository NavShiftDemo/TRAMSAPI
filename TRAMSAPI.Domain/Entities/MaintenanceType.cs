namespace TRAMSAPI.Domain.Entities;

public class MaintenanceType
{
    public long MaintenanceTypeId { get; set; }
    public long TenantId { get; set; }
    public string MaintenanceName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<VehicleMaintenanceRecord> MaintenanceRecords { get; set; } = new List<VehicleMaintenanceRecord>();
}
