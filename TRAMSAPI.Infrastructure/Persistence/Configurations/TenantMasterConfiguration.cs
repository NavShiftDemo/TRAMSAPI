using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class TenantMasterConfiguration : IEntityTypeConfiguration<TenantMaster>
{
    public void Configure(EntityTypeBuilder<TenantMaster> builder)
    {
        builder.ToTable("tb_trams_tenant_master");

        builder.HasKey(e => e.TenantId);
        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.BusinessName)
               .HasColumnName("business_name")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.TenantCode)
               .HasColumnName("tenant_code")
               .HasMaxLength(50)
               .IsRequired();

        builder.HasIndex(e => e.TenantCode)
               .IsUnique();

        builder.Property(e => e.CompanyEmail)
               .HasColumnName("company_email")
               .HasMaxLength(150);

        builder.Property(e => e.CompanyPhoneNo)
               .HasColumnName("company_phone_no")
               .HasMaxLength(20);

        builder.Property(e => e.IsActive)
               .HasColumnName("is_active")
               .HasDefaultValue(true);

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
