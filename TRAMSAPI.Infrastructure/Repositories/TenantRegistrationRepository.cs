
using Microsoft.EntityFrameworkCore;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Domain.Entities;
using TRAMSAPI.Infrastructure.Persistence;

namespace TRAMSAPI.Infrastructure.Repositories;

public class TenantRegistrationRepository : ITenantRegistrationRepository
{
    private readonly ApplicationDbContext _context;

    public TenantRegistrationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task RegisterAsync(
        TenantMaster tenant,
        User owner,
        Role ownerRole,
        UserRoleMapping userRoleMapping)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            // 1. Save tenant and generate TenantId
            await _context.TenantMasters.AddAsync(tenant);
            await _context.SaveChangesAsync();

            // 2. Create owner under the new tenant
            owner.TenantId = tenant.TenantId;
            await _context.Users.AddAsync(owner);

            // 3. Create Owner role under the same tenant
            ownerRole.TenantId = tenant.TenantId;
            await _context.Roles.AddAsync(ownerRole);

            await _context.SaveChangesAsync();

            // 4. Link owner to the Owner role
            userRoleMapping.TenantId = tenant.TenantId;
            userRoleMapping.UserId = owner.UserId;
            userRoleMapping.RoleId = ownerRole.RoleId;
            userRoleMapping.CreatedBy = owner.UserId;

            await _context.UserRoleMappings.AddAsync(userRoleMapping);

            // 5. Record the first owner as tenant creator
            tenant.CreatedBy = owner.UserId;

            await _context.SaveChangesAsync();

            // 6. Complete registration
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
