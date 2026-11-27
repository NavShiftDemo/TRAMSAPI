using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class DriverOperatorDetailConfiguration : IEntityTypeConfiguration<DriverOperatorDetail>
{
    public void Configure(EntityTypeBuilder<DriverOperatorDetail> builder)
    {
        builder.ToTable("tb_trams_driver_operator_detail");

        builder.HasKey(e => e.DriverOperatorId);
        builder.Property(e => e.DriverOperatorId)
               .HasColumnName("driver_operator_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(e => e.LicenseNo)
               .HasColumnName("license_no")
               .HasMaxLength(50);

        builder.Property(e => e.LicenseExpiryDate)
               .HasColumnName("license_expiry_date");

        builder.Property(e => e.ExperienceYears)
               .HasColumnName("experience_years");

        builder.Property(e => e.Specialization)
               .HasColumnName("specialization")
               .HasMaxLength(100);

        builder.Property(e => e.IsActive)
               .HasColumnName("is_active")
               .HasDefaultValue(true);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .IsRequired();

        builder.HasIndex(e => new { e.TenantId, e.UserId })
               .IsUnique();

        builder.HasOne(e => e.Tenant)
               .WithMany(t => t.DriverOperatorDetails)
               .HasForeignKey(e => e.TenantId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.User)
               .WithOne(u => u.DriverOperatorDetail)
               .HasForeignKey<DriverOperatorDetail>(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
