using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

public class SalaryConfig
{
    public long SalaryConfigId { get; set; }
    public long UserId { get; set; }
    public decimal SalaryAmount { get; set; }
    public UnitsType SalaryType { get; set; }
    public long TenantId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
}
