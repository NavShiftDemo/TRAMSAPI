using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMapping>
{
    public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
    {
        builder.ToTable("tb_trams_user_role_mapping");

        builder.HasKey(e => e.UserRoleId);
        builder.Property(e => e.UserRoleId)
               .HasColumnName("user_role_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(e => e.RoleId)
               .HasColumnName("role_id")
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
               .WithMany(t => t.UserRoleMappings)
               .HasForeignKey(e => e.TenantId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.User)
               .WithMany(u => u.UserRoleMappings)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
               .WithMany(r => r.UserRoleMappings)
               .HasForeignKey(e => e.RoleId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
