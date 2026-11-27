using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class AccessPermissionMappingConfiguration : IEntityTypeConfiguration<AccessPermissionMapping>
{
    public void Configure(EntityTypeBuilder<AccessPermissionMapping> builder)
    {
        builder.ToTable("tb_trams_access_permission_mapping");

        builder.HasKey(e => e.AccessPermissionId);
        builder.Property(e => e.AccessPermissionId)
               .HasColumnName("access_permission_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.RoleId)
               .HasColumnName("role_id");

        builder.Property(e => e.UserId)
               .HasColumnName("user_id");

        builder.Property(e => e.ActionId)
               .HasColumnName("action_id")
               .IsRequired();

        builder.Property(e => e.IsActive)
               .HasColumnName("is_active")
               .HasDefaultValue(true);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.HasOne(e => e.Tenant)
               .WithMany(t => t.AccessPermissionMappings)
               .HasForeignKey(e => e.TenantId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Role)
               .WithMany(r => r.AccessPermissionMappings)
               .HasForeignKey(e => e.RoleId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.User)
               .WithMany(u => u.AccessPermissionMappings)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Action)
               .WithMany(a => a.AccessPermissionMappings)
               .HasForeignKey(e => e.ActionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
