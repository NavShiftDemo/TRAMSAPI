namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Global catalogue of application menus shown in the client's UI.
/// </summary>
public class MenuMaster
{
    public long MenuId { get; set; }
    public string MenuName { get; set; } = string.Empty;
    public string MenuKey { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<MenuActionMapping> MenuActionMappings { get; set; } = new List<MenuActionMapping>();
}
