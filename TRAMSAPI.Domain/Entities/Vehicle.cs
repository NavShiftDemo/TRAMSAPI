using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

public class Vehicle
{
    public long VehicleId { get; set; }
    public long TenantId { get; set; }
    public long VehicleTypeId { get; set; }
    public long VehicleBrandId { get; set; }
    public long FuelTypeId { get; set; }
    public string VehicleNumber { get; set; } = string.Empty;
    public string? ModelName { get; set; }
    public int? ManufacturingYear { get; set; }
    public DateOnly? PurchaseDate { get; set; }
    public decimal? PurchasePrice { get; set; }
    public decimal? CurrentKilometers { get; set; }
    public decimal? TopSpeed { get; set; }
    public bool IsImplement { get; set; } = false;
    public OwnerShipType OwnershipType { get; set; }
    public Status VehicleStatus { get; set; }  // Available, Assigned, Maintenance, Inactive
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }

    // Navigation properties
    public virtual VehicleType? VehicleType { get; set; }
    public virtual VehicleBrand? Brand { get; set; }
    public virtual FuelType? FuelType { get; set; }
    public virtual ICollection<VehicleMaintenanceRecord> MaintenanceRecords { get; set; } = new List<VehicleMaintenanceRecord>();
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    public virtual ICollection<RateCard> RateCards { get; set; } = new List<RateCard>();
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
