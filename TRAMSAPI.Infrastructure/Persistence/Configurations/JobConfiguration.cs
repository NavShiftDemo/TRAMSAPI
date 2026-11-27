using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("tb_trams_job");

        builder.HasKey(e => e.JobId);
        builder.Property(e => e.JobId)
               .HasColumnName("job_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.VehicleId)
               .HasColumnName("vehicle_id")
               .IsRequired();

        builder.Property(e => e.DriverId)
               .HasColumnName("driver_id")
               .IsRequired();

        builder.Property(e => e.CustomerId)
               .HasColumnName("customer_id")
               .IsRequired();

        builder.Property(e => e.From)
               .HasColumnName("from")
               .IsRequired();

        builder.Property(e => e.To)
               .HasColumnName("to");

        builder.Property(e => e.AdditionalDetails)
               .HasColumnName("additional_details");

        builder.Property(e => e.TotalAmount)
               .HasColumnName("total_amount")
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(e => e.JobKey)
               .HasColumnName("job_key")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.Property(e => e.UpdatedAt)
               .HasColumnName("updated_at");

        builder.Property(e => e.UpdatedBy)
               .HasColumnName("updated_by");

        builder.HasOne(e => e.Vehicle)
               .WithMany(v => v.Jobs)
               .HasForeignKey(e => e.VehicleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Driver)
               .WithMany()
               .HasForeignKey(e => e.DriverId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
