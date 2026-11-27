using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

public class RateCard
{
    public long RateCardId { get; set; }
    public long TenantId { get; set; }
    public long VehicleId { get; set; }
    public long? ImplementAssetId { get; set; }
    public UnitsType CalculationType_1 { get; set; }
    public UnitsType? CalculationType_2 { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public decimal? WaitingTimeCharge { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }

    // Navigation properties
    public virtual Vehicle? Vehicle { get; set; }
    public virtual ImplementAsset? ImplementAsset { get; set; }
}
