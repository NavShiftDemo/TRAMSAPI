namespace TRAMSAPI.Domain.Entities;

public class Expense
{
    public long ExpenseId { get; set; }
    public long TenantId { get; set; }
    public long ExpenseCategoryId { get; set; }
    public bool IsForVehicle { get; set; } = true;
    public long? VehicleId { get; set; }
    public long? VehicleMaintenanceRecordId { get; set; }
    public DateOnly ExpenseDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string PaymentMode { get; set; } = string.Empty;
    public string? VendorName { get; set; }
    public string? BillNumber { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }

    // Navigation properties
    public virtual ExpenseCategory? ExpenseCategory { get; set; }
    public virtual Vehicle? Vehicle { get; set; }
    public virtual VehicleMaintenanceRecord? VehicleMaintenanceRecord { get; set; }
}
