using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class JobPaymentConfiguration : IEntityTypeConfiguration<JobPayment>
{
    public void Configure(EntityTypeBuilder<JobPayment> builder)
    {
        builder.ToTable("tb_trams_job_payment");

        builder.HasKey(e => e.JobPaymentId);
        builder.Property(e => e.JobPaymentId)
               .HasColumnName("job_payment_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.JobKey)
               .HasColumnName("job_key")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.CollectedAmount)
               .HasColumnName("collected_amount")
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(e => e.Discount)
               .HasColumnName("discount")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.SettlementStatus)
               .HasColumnName("settlement_status")
               .HasConversion<int>();

        builder.Property(e => e.PaymentType)
               .HasColumnName("payment_type")
               .HasMaxLength(50);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.Property(e => e.UpdatedAt)
               .HasColumnName("updated_at");

        builder.Property(e => e.UpdatedBy)
               .HasColumnName("updated_by");
    }
}
