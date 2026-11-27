using Microsoft.Extensions.Logging;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Infrastructure.Services;

/// <summary>
/// Local logger implementation of IOtpNotificationService that outputs OTP codes directly to application logs.
/// </summary>
public class NoOpOtpNotificationService : IOtpNotificationService
{
    private readonly ILogger<NoOpOtpNotificationService> _logger;

    public NoOpOtpNotificationService(ILogger<NoOpOtpNotificationService> logger)
    {
        _logger = logger;
    }

    public Task<bool> SendOtpAsync(string destination, string otpCode, Purpose purpose)
    {
        _logger.LogInformation(">>> [OTP NOTIFICATION] Destination: {Destination} | Code: {Code} | Purpose: {Purpose}",
            destination, otpCode, purpose);
        return Task.FromResult(true);
    }
}
