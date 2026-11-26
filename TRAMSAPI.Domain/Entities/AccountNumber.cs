namespace TRAMSAPI.Domain.Entities;

public class AccountNumber
{
    public long AccountId { get; set; }
    public long TenantId { get; set; }
    public long UserId { get; set; }
    public string AccountNo { get; set; } = string.Empty;
    public int Nonencrypted { get; set; } = string.Empty;
    public string BankName { get; set; } = string.Empty;
    public string IFSC { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long? CreatedBy { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
}
