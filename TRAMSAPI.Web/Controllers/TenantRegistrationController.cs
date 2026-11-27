using Microsoft.AspNetCore.Mvc;
using TRAMSAPI.Application.DTOs;
using TRAMSAPI.Application.Interfaces;

namespace TRAMSAPI.Web.Controllers;

[ApiController]
[Route("api/tenant-registration")]
public class TenantRegistrationController : ControllerBase
{
    private readonly ITenantRegistrationService _registrationService;

    public TenantRegistrationController(
        ITenantRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] TenantRegistrationRequestDto request)
    {
        var result = await _registrationService.RegisterTenantAsync(request);

        if (!result)
        {
            return BadRequest(new
            {
                message = "Tenant registration failed."
            });
        }

        return Ok(new
        {
            message = "Company and owner registered successfully."
        });
    }
}
