namespace TRAMSAPI.Domain.Entities;

public class VehicleType
{
    public long VehicleTypeId { get; set; }
    public long TenantId { get; set; }
    public long VehicleCategoryId { get; set; }
    public string TypeName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual VehicleCategory? Category { get; set; }
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
