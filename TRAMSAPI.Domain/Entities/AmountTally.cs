using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

public class AmountTally
{
    public long AmountTallyId { get; set; }
    public long TenantId { get; set; }
    public long UserId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal? Incentive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
}
