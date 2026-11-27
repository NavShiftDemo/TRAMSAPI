using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Application.Interfaces;

public interface IOtpNotificationService
{
    Task<bool> SendOtpAsync(string destination, string otpCode, Purpose purpose);
}
