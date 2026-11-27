using Microsoft.EntityFrameworkCore;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Infrastructure.Persistence.Seeds;

public static class UserSeedData
{
    // Deterministic salt (29 chars) for password "Admin" so EF Core migration remains deterministic
    private static readonly string AdminPasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin", "$2a$11$e8xr3VpVy9w5RZZaH6N5qu");

    public static void Seed(ModelBuilder modelBuilder)
    {
        var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // 1. Default Tenant
        modelBuilder.Entity<TenantMaster>().HasData(
            new TenantMaster
            {
                TenantId = 1,
                BusinessName = "NavShift System",
                TenantCode = "DEFAULT",
                CompanyEmail = "admin123@gmail.com",
                CompanyPhoneNo = "0000000000",
                IsActive = true,
                CreatedAt = seedDate
            }
        );

        // 2. Developer Role
        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                RoleId = 1,
                TenantId = 1,
                RoleName = "Developer",
                Description = "Developer with system administrative privileges",
                IsActive = true,
                CreatedAt = seedDate
            }
        );

        // 3. First User: Admin (Password: Admin hashed via BCrypt)
        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                TenantId = 1,
                Name = "Admin",
                Email = "admin123@gmail.com",
                PhoneNo = "0000000000",
                PasswordHash = AdminPasswordHash,
                IsSystemUser = true,
                IsActive = true,
                CreatedAt = seedDate
            }
        );

        // 4. Assign User to Role
        modelBuilder.Entity<UserRoleMapping>().HasData(
            new UserRoleMapping
            {
                UserRoleId = 1,
                TenantId = 1,
                UserId = 1,
                RoleId = 1,
                IsActive = true,
                CreatedAt = seedDate
            }
        );
    }
}
