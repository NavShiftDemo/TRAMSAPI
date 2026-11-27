using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class ActionMasterConfiguration : IEntityTypeConfiguration<ActionMaster>
{
    public void Configure(EntityTypeBuilder<ActionMaster> builder)
    {
        builder.ToTable("tb_trams_action_master");

        builder.HasKey(e => e.ActionId);
        builder.Property(e => e.ActionId)
               .HasColumnName("action_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.ActionName)
               .HasColumnName("action_name")
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
               .HasColumnName("created_by");
    }
}
