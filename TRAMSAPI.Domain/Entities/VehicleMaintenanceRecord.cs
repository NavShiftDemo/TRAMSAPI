namespace TRAMSAPI.Domain.Entities;

public class VehicleMaintenanceRecord
{
    public long VehicleMaintenanceRecordId { get; set; }
    public long TenantId { get; set; }
    public long VehicleId { get; set; }
    public long MaintenanceTypeId { get; set; }
    public DateOnly ServiceDate { get; set; }
    public decimal? ServiceReading { get; set; }
    public string? ServicePerformedBy { get; set; }
    public string? ServiceProviderName { get; set; }
    public string? Description { get; set; }
    public decimal? NextServiceReading { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual Vehicle? Vehicle { get; set; }
    public virtual MaintenanceType? MaintenanceType { get; set; }
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
