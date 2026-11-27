using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("tb_trams_role");

        builder.HasKey(e => e.RoleId);
        builder.Property(e => e.RoleId)
               .HasColumnName("role_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.RoleName)
               .HasColumnName("role_name")
               .HasMaxLength(50)
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
               .HasColumnName("created_by");

        builder.HasIndex(e => new { e.TenantId, e.RoleName })
               .IsUnique();

        builder.HasOne(e => e.Tenant)
               .WithMany(t => t.Roles)
               .HasForeignKey(e => e.TenantId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
