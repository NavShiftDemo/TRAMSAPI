using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class TableListConfiguration : IEntityTypeConfiguration<TableList>
{
    public void Configure(EntityTypeBuilder<TableList> builder)
    {
        builder.ToTable("tb_trams_table_list");

        builder.HasKey(e => e.TableListId);
        builder.Property(e => e.TableListId)
               .HasColumnName("table_list_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TableName)
               .HasColumnName("table_name")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();
    }
}
