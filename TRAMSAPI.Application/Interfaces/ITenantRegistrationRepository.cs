
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Application.Interfaces;

public interface ITenantRegistrationRepository
{
    Task RegisterAsync(
        TenantMaster tenant,
        User owner,
        Role ownerRole,
        UserRoleMapping userRoleMapping
    );
}
