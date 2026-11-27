using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("tb_trams_expense");

        builder.HasKey(e => e.ExpenseId);
        builder.Property(e => e.ExpenseId)
               .HasColumnName("expense_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.ExpenseCategoryId)
               .HasColumnName("expense_category_id")
               .IsRequired();

        builder.Property(e => e.IsForVehicle)
               .HasColumnName("is_for_vehicle")
               .HasDefaultValue(true);

        builder.Property(e => e.VehicleId)
               .HasColumnName("vehicle_id");

        builder.Property(e => e.VehicleMaintenanceRecordId)
               .HasColumnName("vehicle_maintenance_record_id");

        builder.Property(e => e.ExpenseDate)
               .HasColumnName("expense_date")
               .IsRequired();

        builder.Property(e => e.Title)
               .HasColumnName("title")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.Amount)
               .HasColumnName("amount")
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(e => e.PaymentMode)
               .HasColumnName("payment_mode")
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.VendorName)
               .HasColumnName("vendor_name")
               .HasMaxLength(150);

        builder.Property(e => e.BillNumber)
               .HasColumnName("bill_number")
               .HasMaxLength(100);

        builder.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(500);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .IsRequired();

        builder.HasOne(e => e.ExpenseCategory)
               .WithMany(c => c.Expenses)
               .HasForeignKey(e => e.ExpenseCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Vehicle)
               .WithMany(v => v.Expenses)
               .HasForeignKey(e => e.VehicleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.VehicleMaintenanceRecord)
               .WithMany(m => m.Expenses)
               .HasForeignKey(e => e.VehicleMaintenanceRecordId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
