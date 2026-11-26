namespace TRAMSAPI.Domain.Entities;

public class FuelType
{
    public long FuelTypeId { get; set; }
    public long TenantId { get; set; }
    public string FuelName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
