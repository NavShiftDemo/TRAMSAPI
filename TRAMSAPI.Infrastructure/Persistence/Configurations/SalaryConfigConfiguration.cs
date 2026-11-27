using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class SalaryConfigConfiguration : IEntityTypeConfiguration<SalaryConfig>
{
    public void Configure(EntityTypeBuilder<SalaryConfig> builder)
    {
        builder.ToTable("tb_trams_salary_config");

        builder.HasKey(e => e.SalaryConfigId);
        builder.Property(e => e.SalaryConfigId)
               .HasColumnName("salary_config_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(e => e.SalaryAmount)
               .HasColumnName("salary_amount")
               .HasColumnType("numeric(18,2)")
               .IsRequired();

        builder.Property(e => e.SalaryType)
               .HasColumnName("salary_type")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.HasOne(e => e.User)
               .WithMany(u => u.SalaryConfigs)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
