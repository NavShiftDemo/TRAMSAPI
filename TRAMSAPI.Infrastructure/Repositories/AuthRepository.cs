using Microsoft.EntityFrameworkCore;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Domain.Entities;
using TRAMSAPI.Domain.Enums;
using TRAMSAPI.Infrastructure.Persistence;

namespace TRAMSAPI.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;

    public AuthRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdentifierAsync(string identifier)
    {
        return await _context.Users
            .Include(u => u.Tenant)
            .Include(u => u.UserRoleMappings)
                .ThenInclude(urm => urm.Role)
            .FirstOrDefaultAsync(u => u.Email == identifier || u.PhoneNo == identifier);
    }

    public async Task<User?> GetUserByIdAsync(long userId)
    {
        return await _context.Users
            .Include(u => u.Tenant)
            .Include(u => u.UserRoleMappings)
                .ThenInclude(urm => urm.Role)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task RecordLoginHistoryAsync(UserLoginHistory history)
    {
        await _context.UserLoginHistories.AddAsync(history);
        await _context.SaveChangesAsync();
    }

    public async Task SaveOtpAsync(UserOtpVerification otp)
    {
        // Invalidate older pending OTPs for the same user and purpose
        var pendingOtps = await _context.UserOtpVerifications
            .Where(o => o.UserId == otp.UserId && o.Purpose == otp.Purpose && !o.IsVerified)
            .ToListAsync();

        foreach (var pending in pendingOtps)
        {
            pending.AttemptCount = 99; // Invalidate
        }

        await _context.UserOtpVerifications.AddAsync(otp);
        await _context.SaveChangesAsync();
    }

    public async Task<UserOtpVerification?> GetLatestOtpAsync(long userId, Purpose purpose)
    {
        return await _context.UserOtpVerifications
            .Where(o => o.UserId == userId && o.Purpose == purpose)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateOtpAsync(UserOtpVerification otp)
    {
        _context.UserOtpVerifications.Update(otp);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdatePasswordAsync(long userId, string newHashedPassword)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;

        user.PasswordHash = newHashedPassword;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }
}
