namespace TRAMSAPI.Domain.Entities;

public class Job
{
    public long JobId { get; set; }
    public long TenantId { get; set; }
    public long VehicleId { get; set; }
    public long DriverId { get; set; }
    public long CustomerId { get; set; }
    public int From { get; set; }
    public int? To { get; set; }
    public int? AdditionalDetails { get; set; }
    public decimal TotalAmount { get; set; }
    public string JobKey { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }

    // Navigation properties
    public virtual Vehicle? Vehicle { get; set; }
    public virtual User? Driver { get; set; }
    public virtual ICollection<AdditionalJob> AdditionalJobs { get; set; } = new List<AdditionalJob>();
}
