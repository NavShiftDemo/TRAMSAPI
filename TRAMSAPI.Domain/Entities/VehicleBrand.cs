namespace TRAMSAPI.Domain.Entities;

public class VehicleBrand
{
    public long VehicleBrandId { get; set; }
    public long TenantId { get; set; }
    public string BrandName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
