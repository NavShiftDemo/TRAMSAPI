using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
{
    public void Configure(EntityTypeBuilder<ErrorLog> builder)
    {
        builder.ToTable("tb_trams_error_log");

        builder.HasKey(e => e.ErrorLogId);
        builder.Property(e => e.ErrorLogId)
               .HasColumnName("error_log_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id");

        builder.Property(e => e.UserId)
               .HasColumnName("user_id");

        builder.Property(e => e.RequestPath)
               .HasColumnName("request_path")
               .HasMaxLength(500);

        builder.Property(e => e.HttpMethod)
               .HasColumnName("http_method")
               .HasConversion<int>();

        builder.Property(e => e.StatusCode)
               .HasColumnName("status_code");

        builder.Property(e => e.ErrorMessage)
               .HasColumnName("error_message");

        builder.Property(e => e.ExceptionType)
               .HasColumnName("exception_type")
               .HasMaxLength(255);

        builder.Property(e => e.StackTrace)
               .HasColumnName("stack_trace");

        builder.Property(e => e.Source)
               .HasColumnName("source")
               .HasMaxLength(255);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();
    }
}
