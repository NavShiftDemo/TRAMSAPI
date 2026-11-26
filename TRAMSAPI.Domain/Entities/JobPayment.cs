namespace TRAMSAPI.Domain.Entities;

public class JobPayment
{
    public long JobPaymentId { get; set; }
    public long TenantId { get; set; }
    public string JobKey { get; set; } = string.Empty;
    public decimal CollectedAmount { get; set; }
    public decimal? Discount { get; set; }
    public string? SettlementStatus { get; set; }
    public string? PaymentType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
}
