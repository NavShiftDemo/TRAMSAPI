using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class MenuActionMappingConfiguration : IEntityTypeConfiguration<MenuActionMapping>
{
    public void Configure(EntityTypeBuilder<MenuActionMapping> builder)
    {
        builder.ToTable("tb_trams_menu_action_mapping");

        builder.HasKey(e => e.MenuActionMappingId);
        builder.Property(e => e.MenuActionMappingId)
               .HasColumnName("menu_action_mapping_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.MenuId)
               .HasColumnName("menu_id")
               .IsRequired();

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

        builder.HasOne(e => e.Menu)
               .WithMany(m => m.MenuActionMappings)
               .HasForeignKey(e => e.MenuId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Action)
               .WithMany(a => a.MenuActionMappings)
               .HasForeignKey(e => e.ActionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
