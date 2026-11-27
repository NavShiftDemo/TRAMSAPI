using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRAMSAPI.Infrastructure.Persistence;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Infrastructure.Repositories;

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

        // Tenant Registration Repository
        services.AddScoped<ITenantRegistrationRepository, TenantRegistrationRepository>();

        return services;
    }
}
