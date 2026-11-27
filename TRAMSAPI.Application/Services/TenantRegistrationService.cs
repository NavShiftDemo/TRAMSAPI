using TRAMSAPI.Application.DTOs;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Domain.Entities;

namespace TRAMSAPI.Application.Services;

public class TenantRegistrationService : ITenantRegistrationService
{
    private readonly ITenantRegistrationRepository _repository;

    public TenantRegistrationService(ITenantRegistrationRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> RegisterTenantAsync(TenantRegistrationRequestDto request)
    {
        var now = DateTime.UtcNow;

        // Create tenant
        var tenant = new TenantMaster
        {
            BusinessName = request.BusinessName,
            TenantCode = Guid.NewGuid().ToString("N")[..12].ToUpper(),
            CompanyEmail = request.CompanyEmail,
            CompanyPhoneNo = request.CompanyPhoneNo,
            IsActive = true,
            CreatedAt = now
        };

        // Create first owner
        var owner = new User
        {
            Name = request.OwnerName,
            Email = request.OwnerEmail,
            PhoneNo = request.OwnerPhoneNo,
            AlternatePhoneNo = request.AlternatePhoneNo,
            Address = request.Address,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            IsSystemUser = false,
            IsActive = true,
            CreatedAt = now
        };

        // Create tenant-specific Owner role
        var ownerRole = new Role
        {
            RoleName = "Owner",
            Description = "Primary owner of the registered business",
            IsActive = true,
            CreatedAt = now
        };

        // Link owner to Owner role
        var userRoleMapping = new UserRoleMapping
        {
            IsActive = true,
            CreatedAt = now
        };

        await _repository.RegisterAsync(
            tenant,
            owner,
            ownerRole,
            userRoleMapping
        );

        return true;
    }
}
