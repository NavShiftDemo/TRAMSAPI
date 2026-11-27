using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class VehicleMaintenanceRecordConfiguration : IEntityTypeConfiguration<VehicleMaintenanceRecord>
{
    public void Configure(EntityTypeBuilder<VehicleMaintenanceRecord> builder)
    {
        builder.ToTable("tb_trams_vehicle_maintenance_record");

        builder.HasKey(e => e.VehicleMaintenanceRecordId);
        builder.Property(e => e.VehicleMaintenanceRecordId)
               .HasColumnName("vehicle_maintenance_record_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.VehicleId)
               .HasColumnName("vehicle_id")
               .IsRequired();

        builder.Property(e => e.MaintenanceTypeId)
               .HasColumnName("maintenance_type_id")
               .IsRequired();

        builder.Property(e => e.ServiceDate)
               .HasColumnName("service_date")
               .IsRequired();

        builder.Property(e => e.ServiceReading)
               .HasColumnName("service_reading")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.ServicePerformedBy)
               .HasColumnName("service_performed_by")
               .HasMaxLength(150);

        builder.Property(e => e.ServiceProviderName)
               .HasColumnName("service_provider_name")
               .HasMaxLength(150);

        builder.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(500);

        builder.Property(e => e.NextServiceReading)
               .HasColumnName("next_service_reading")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .IsRequired();

        builder.HasOne(e => e.Vehicle)
               .WithMany(v => v.MaintenanceRecords)
               .HasForeignKey(e => e.VehicleId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.MaintenanceType)
               .WithMany(m => m.MaintenanceRecords)
               .HasForeignKey(e => e.MaintenanceTypeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
