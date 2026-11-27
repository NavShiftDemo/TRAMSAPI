using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class UserLoginHistoryConfiguration : IEntityTypeConfiguration<UserLoginHistory>
{
    public void Configure(EntityTypeBuilder<UserLoginHistory> builder)
    {
        builder.ToTable("tb_trams_user_login_history");

        builder.HasKey(e => e.LoginHistoryId);
        builder.Property(e => e.LoginHistoryId)
               .HasColumnName("login_history_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id");

        builder.Property(e => e.UserId)
               .HasColumnName("user_id");

        builder.Property(e => e.EventType)
               .HasColumnName("event_type")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.EventTime)
               .HasColumnName("event_time")
               .IsRequired();

        builder.Property(e => e.IpAddress)
               .HasColumnName("ip_address")
               .HasMaxLength(50);

        builder.Property(e => e.DeviceInfo)
               .HasColumnName("device_info")
               .HasMaxLength(255);

        builder.HasOne(e => e.Tenant)
               .WithMany()
               .HasForeignKey(e => e.TenantId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.User)
               .WithMany(u => u.LoginHistories)
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
