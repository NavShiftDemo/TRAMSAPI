using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class DataAuditConfiguration : IEntityTypeConfiguration<DataAudit>
{
    public void Configure(EntityTypeBuilder<DataAudit> builder)
    {
        builder.ToTable("tb_trams_data_audit");

        builder.HasKey(e => e.DataAuditId);
        builder.Property(e => e.DataAuditId)
               .HasColumnName("data_audit_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id");

        builder.Property(e => e.TableName)
               .HasColumnName("table_name")
               .HasMaxLength(150)
               .IsRequired();

        builder.Property(e => e.RecordId)
               .HasColumnName("record_id")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.Action)
               .HasColumnName("action")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.OldValues)
               .HasColumnName("old_values");

        builder.Property(e => e.NewValues)
               .HasColumnName("new_values");

        builder.Property(e => e.ChangedColumns)
               .HasColumnName("changed_columns")
               .HasMaxLength(500);

        builder.Property(e => e.UserId)
               .HasColumnName("user_id");

        builder.Property(e => e.Timestamp)
               .HasColumnName("timestamp")
               .IsRequired();
    }
}
