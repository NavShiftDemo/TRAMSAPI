namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Defines the individual actions available under each menu.
/// </summary>
public class ActionMaster
{
    public long ActionId { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual ICollection<MenuActionMapping> MenuActionMappings { get; set; } = new List<MenuActionMapping>();
    public virtual ICollection<AccessPermissionMapping> AccessPermissionMappings { get; set; } = new List<AccessPermissionMapping>();
}
