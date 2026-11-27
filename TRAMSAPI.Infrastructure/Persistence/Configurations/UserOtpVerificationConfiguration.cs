using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class UserOtpVerificationConfiguration : IEntityTypeConfiguration<UserOtpVerification>
{
    public void Configure(EntityTypeBuilder<UserOtpVerification> builder)
    {
        builder.ToTable("tb_trams_user_otp_verification");

        builder.HasKey(e => e.OtpId);
        builder.Property(e => e.OtpId)
               .HasColumnName("otp_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(e => e.OtpHash)
               .HasColumnName("otp_hash")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(e => e.Purpose)
               .HasColumnName("purpose")
               .HasConversion<int>()
               .IsRequired();

        builder.Property(e => e.IsVerified)
               .HasColumnName("is_verified")
               .HasDefaultValue(false);

        builder.Property(e => e.AttemptCount)
               .HasColumnName("attempt_count")
               .HasDefaultValue(0);

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
