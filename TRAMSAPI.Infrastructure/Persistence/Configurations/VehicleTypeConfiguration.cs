using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.ToTable("tb_trams_vehicle_type");

        builder.HasKey(e => e.VehicleTypeId);
        builder.Property(e => e.VehicleTypeId)
               .HasColumnName("vehicle_type_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.VehicleCategoryId)
               .HasColumnName("vehicle_category_id")
               .IsRequired();

        builder.Property(e => e.TypeName)
               .HasColumnName("type_name")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(255);

        builder.Property(e => e.IsActive)
               .HasColumnName("is_active")
               .HasDefaultValue(true);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .IsRequired();

        builder.HasOne(e => e.Category)
               .WithMany(c => c.VehicleTypes)
               .HasForeignKey(e => e.VehicleCategoryId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
