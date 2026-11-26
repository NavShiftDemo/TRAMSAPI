namespace TRAMSAPI.Domain.Entities;

public class VehicleCategory
{
    public long VehicleCategoryId { get; set; }
    public long TenantId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<VehicleType> VehicleTypes { get; set; } = new List<VehicleType>();
}
