using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRAMSAPI.Infrastructure.Persistence;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Infrastructure.Repositories;
using TRAMSAPI.Infrastructure.Services;

namespace TRAMSAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        // Repositories
        services.AddScoped<ITenantRegistrationRepository, TenantRegistrationRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        // Infrastructure Services
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IOtpNotificationService, EmailNotificationService>();

        return services;
    }
}
