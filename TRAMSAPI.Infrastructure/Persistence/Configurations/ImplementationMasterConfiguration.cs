using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class ImplementationMasterConfiguration : IEntityTypeConfiguration<ImplementationMaster>
{
    public void Configure(EntityTypeBuilder<ImplementationMaster> builder)
    {
        builder.ToTable("tb_trams_implementation_master");

        builder.HasKey(e => e.ImplementationMasterId);
        builder.Property(e => e.ImplementationMasterId)
               .HasColumnName("implementation_master_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.VehicleCategoryId)
               .HasColumnName("vehicle_category_id");

        builder.Property(e => e.ImplementationName)
               .HasColumnName("implementation_name")
               .HasMaxLength(150)
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
    }
}
