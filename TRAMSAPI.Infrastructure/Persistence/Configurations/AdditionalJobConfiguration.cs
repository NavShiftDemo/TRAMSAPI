using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class AdditionalJobConfiguration : IEntityTypeConfiguration<AdditionalJob>
{
    public void Configure(EntityTypeBuilder<AdditionalJob> builder)
    {
        builder.ToTable("tb_trams_additional_job");

        builder.HasKey(e => e.WaitingId);
        builder.Property(e => e.WaitingId)
               .HasColumnName("waiting_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.JobId)
               .HasColumnName("job_id")
               .IsRequired();

        builder.Property(e => e.WaitingStartTime)
               .HasColumnName("waiting_start_time")
               .IsRequired();

        builder.Property(e => e.WaitingEndTime)
               .HasColumnName("waiting_end_time")
               .IsRequired();

        builder.Property(e => e.Amount)
               .HasColumnName("amount")
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(e => e.Reason)
               .HasColumnName("reason")
               .HasMaxLength(255);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.HasOne(e => e.Job)
               .WithMany(j => j.AdditionalJobs)
               .HasForeignKey(e => e.JobId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
