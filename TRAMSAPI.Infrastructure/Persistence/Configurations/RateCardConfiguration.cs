using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class RateCardConfiguration : IEntityTypeConfiguration<RateCard>
{
    public void Configure(EntityTypeBuilder<RateCard> builder)
    {
        builder.ToTable("tb_trams_rate_card");

        builder.HasKey(e => e.RateCardId);
        builder.Property(e => e.RateCardId)
               .HasColumnName("rate_card_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.VehicleId)
               .HasColumnName("vehicle_id")
               .IsRequired();

        builder.Property(e => e.ImplementAssetId)
               .HasColumnName("implement_asset_id");

        builder.Property(e => e.CalculationType_1)
               .HasColumnName("calculation_type_1")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.CalculationType_2)
               .HasColumnName("calculation_type_2")
               .HasConversion<int>();

        builder.Property(e => e.Amount)
               .HasColumnName("amount")
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(500);

        builder.Property(e => e.WaitingTimeCharge)
               .HasColumnName("waiting_time_charge")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.Property(e => e.UpdatedAt)
               .HasColumnName("updated_at");

        builder.Property(e => e.UpdatedBy)
               .HasColumnName("updated_by");

        builder.HasOne(e => e.Vehicle)
               .WithMany(v => v.RateCards)
               .HasForeignKey(e => e.VehicleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.ImplementAsset)
               .WithMany()
               .HasForeignKey(e => e.ImplementAssetId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
