using TRAMSAPI.Application.DTOs;

namespace TRAMSAPI.Application.Interfaces;

public interface ITenantRegistrationService
{
    Task<bool> RegisterTenantAsync(TenantRegistrationRequestDto request);
}
