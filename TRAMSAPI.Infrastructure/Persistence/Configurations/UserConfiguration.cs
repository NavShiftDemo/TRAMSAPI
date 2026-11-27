using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("tb_trams_user");

        builder.HasKey(e => e.UserId);
        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.Name)
               .HasColumnName("name")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.Email)
               .HasColumnName("email")
               .HasMaxLength(150);

        builder.Property(e => e.PhoneNo)
               .HasColumnName("phone_no")
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(e => e.AlternatePhoneNo)
               .HasColumnName("alternate_phone_no")
               .HasMaxLength(20);

        builder.Property(e => e.Address)
               .HasColumnName("address")
               .HasMaxLength(255);

        builder.Property(e => e.ProfileImageMime)
               .HasColumnName("profile_image_mime")
               .HasMaxLength(100);

        builder.Property(e => e.PasswordHash)
               .HasColumnName("password_hash")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(e => e.IsSystemUser)
               .HasColumnName("is_system_user")
               .HasDefaultValue(false);

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

        builder.HasOne(e => e.Tenant)
               .WithMany(t => t.Users)
               .HasForeignKey(e => e.TenantId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
