using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class ImplementAssetConfiguration : IEntityTypeConfiguration<ImplementAsset>
{
    public void Configure(EntityTypeBuilder<ImplementAsset> builder)
    {
        builder.ToTable("tb_trams_implement_asset");

        builder.HasKey(e => e.ImplementAssetId);
        builder.Property(e => e.ImplementAssetId)
               .HasColumnName("implement_asset_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.ImplementMasterId)
               .HasColumnName("implement_master_id");

        builder.Property(e => e.ImplementCode)
               .HasColumnName("implement_code")
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.Name)
               .HasColumnName("name")
               .HasMaxLength(150);

        builder.Property(e => e.OwnershipType)
               .HasColumnName("ownership_type")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.Status)
               .HasColumnName("status")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.ImageUrl)
               .HasColumnName("image_url")
               .HasMaxLength(500);

        builder.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(500);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .IsRequired();
    }
}
