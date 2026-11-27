using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class AmountTallyConfiguration : IEntityTypeConfiguration<AmountTally>
{
    public void Configure(EntityTypeBuilder<AmountTally> builder)
    {
        builder.ToTable("tb_trams_amount_tally");

        builder.HasKey(e => e.AmountTallyId);
        builder.Property(e => e.AmountTallyId)
               .HasColumnName("amount_tally_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(e => e.Amount)
               .HasColumnName("amount")
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(e => e.TransactionType)
               .HasColumnName("transaction_type")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.Incentive)
               .HasColumnName("incentive")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
