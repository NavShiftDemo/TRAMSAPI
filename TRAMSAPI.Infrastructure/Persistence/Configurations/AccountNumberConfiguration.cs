using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Configurations;

public class AccountNumberConfiguration : IEntityTypeConfiguration<AccountNumber>
{
    public void Configure(EntityTypeBuilder<AccountNumber> builder)
    {
        builder.ToTable("tb_trams_account_number");

        builder.HasKey(e => e.AccountId);
        builder.Property(e => e.AccountId)
               .HasColumnName("account_id")
               .ValueGeneratedOnAdd();

        builder.Property(e => e.TenantId)
               .HasColumnName("tenant_id")
               .IsRequired();

        builder.Property(e => e.UserId)
               .HasColumnName("user_id")
               .IsRequired();

        builder.Property(e => e.AccountNo)
               .HasColumnName("account_no")
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.Nonencrypted)
               .HasColumnName("nonencrypted")
               .IsRequired();

        builder.Property(e => e.BankName)
               .HasColumnName("bank_name")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.IFSC)
               .HasColumnName("ifsc")
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(e => e.BranchName)
               .HasColumnName("branch_name")
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.MobileNumber)
               .HasColumnName("mobile_number")
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(e => e.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        builder.Property(e => e.CreatedBy)
               .HasColumnName("created_by");

        builder.HasOne(e => e.User)
               .WithMany()
               .HasForeignKey(e => e.UserId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
