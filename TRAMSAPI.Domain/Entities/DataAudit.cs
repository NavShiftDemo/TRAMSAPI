using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores audit records of all insert, update, and delete operations on entities.
/// </summary>
public class DataAudit
{
    public long DataAuditId { get; set; }
    public long? TenantId { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string RecordId { get; set; } = string.Empty;
    public AuditActionType Action { get; set; }
    public string? OldValues { get; set; } // JSON serialized old data
    public string? NewValues { get; set; } // JSON serialized new data
    public string? ChangedColumns { get; set; }
    public long? UserId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
