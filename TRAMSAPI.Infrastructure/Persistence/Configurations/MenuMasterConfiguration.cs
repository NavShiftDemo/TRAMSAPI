using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class MenuMasterConfiguration : IEntityTypeConfiguration<MenuMaster>
{
    public void Configure(EntityTypeBuilder<MenuMaster> builder)
    {
        builder.ToTable("tb_trams_menu_master");

        builder.HasKey(e => e.MenuId);
        builder.Property(e => e.MenuId)
               .HasColumnName("menu_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.MenuName)
               .HasColumnName("menu_name")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.MenuKey)
               .HasColumnName("menu_key")
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(e => e.MenuKey)
               .IsUnique();

        builder.Property(e => e.IsActive)
               .HasColumnName("is_active")
               .HasDefaultValue(true);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");
    }
}
