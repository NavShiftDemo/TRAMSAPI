
using Microsoft.EntityFrameworkCore;
using TRAMSAPI.Infrastructure;
using TRAMSAPI.Infrastructure.Persistence;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure Persistence Dependency Injection
builder.Services.AddInfrastructureServices(builder.Configuration);

// Tenant Registration Dependency Injection
builder.Services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Health check endpoint to verify database connectivity and seed data
app.MapGet("/api/health", async (ApplicationDbContext db) =>
{
    var canConnect = await db.Database.CanConnectAsync();
    var tableCount = await db.TablesList.CountAsync();
    var adminUser = await db.Users
        .Select(u => new { u.UserId, u.Name, u.Email, u.IsActive })
        .FirstOrDefaultAsync();

    return Results.Ok(new
    {
        status = "Healthy",
        databaseConnected = canConnect,
        totalTablesSeeded = tableCount,
        seededAdminUser = adminUser
    });
})
.WithName("HealthCheck")
.WithOpenApi();

// Map API Controllers
app.MapControllers();

app.Run();
