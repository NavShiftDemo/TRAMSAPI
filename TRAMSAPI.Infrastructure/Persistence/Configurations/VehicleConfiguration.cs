using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("tb_trams_vehicle");

        builder.HasKey(e => e.VehicleId);
        builder.Property(e => e.VehicleId)
               .HasColumnName("vehicle_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.VehicleTypeId)
               .HasColumnName("vehicle_type_id")
               .IsRequired();

        builder.Property(e => e.VehicleBrandId)
               .HasColumnName("vehicle_brand_id")
               .IsRequired();

        builder.Property(e => e.FuelTypeId)
               .HasColumnName("fuel_type_id")
               .IsRequired();

        builder.Property(e => e.VehicleNumber)
               .HasColumnName("vehicle_number")
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.ModelName)
               .HasColumnName("model_name")
               .HasMaxLength(100);

        builder.Property(e => e.ManufacturingYear)
               .HasColumnName("manufacturing_year");

        builder.Property(e => e.PurchaseDate)
               .HasColumnName("purchase_date");

        builder.Property(e => e.PurchasePrice)
               .HasColumnName("purchase_price")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.CurrentKilometers)
               .HasColumnName("current_kilometers")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.TopSpeed)
               .HasColumnName("top_speed")
               .HasColumnType("numeric(18,2)");

        builder.Property(e => e.IsImplement)
               .HasColumnName("is_implement")
               .HasDefaultValue(false);

        builder.Property(e => e.OwnershipType)
               .HasColumnName("ownership_type")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.VehicleStatus)
               .HasColumnName("vehicle_status")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.ImageUrl)
               .HasColumnName("image_url")
               .HasMaxLength(500);

        builder.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(500);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .IsRequired();

        builder.Property(e => e.UpdatedAt)
               .HasColumnName("updated_at");

        builder.Property(e => e.UpdatedBy)
               .HasColumnName("updated_by");

        builder.HasOne(e => e.VehicleType)
               .WithMany(t => t.Vehicles)
               .HasForeignKey(e => e.VehicleTypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Brand)
               .WithMany(b => b.Vehicles)
               .HasForeignKey(e => e.VehicleBrandId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.FuelType)
               .WithMany(f => f.Vehicles)
               .HasForeignKey(e => e.FuelTypeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
