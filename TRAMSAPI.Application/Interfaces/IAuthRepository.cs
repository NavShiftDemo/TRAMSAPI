using TRAMSAPI.Domain.Entities;
using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Application.Interfaces;

public interface IAuthRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(long userId);
    Task RecordLoginHistoryAsync(UserLoginHistory history);
    Task SaveOtpAsync(UserOtpVerification otp);
    Task<UserOtpVerification?> GetLatestOtpAsync(long userId, Purpose purpose);
    Task UpdateOtpAsync(UserOtpVerification otp);
    Task<bool> UpdatePasswordAsync(long userId, string newHashedPassword);
}
