namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Links actions to menu items.
/// </summary>
public class MenuActionMapping
{
    public long MenuActionMappingId { get; set; }
    public long MenuId { get; set; }
    public long ActionId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual MenuMaster? Menu { get; set; }
    public virtual ActionMaster? Action { get; set; }
}
