namespace TRAMSAPI.Domain.Entities;

public class AdditionalJob
{
    public long WaitingId { get; set; }
    public long TenantId { get; set; }
    public long JobId { get; set; }
    public DateTime WaitingStartTime { get; set; }
    public DateTime WaitingEndTime { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }
    public string? Reason { get; set; }

    // Navigation properties
    public virtual Job? Job { get; set; }
}
