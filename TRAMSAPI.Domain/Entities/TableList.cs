namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Registry of database tables and modules managed by the system.
/// </summary>
public class TableList
{
    public long TableListId { get; set; }
    public string TableName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
