using Microsoft.EntityFrameworkCore;
using TRAMSAPI.Infrastructure;
using TRAMSAPI.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Infrastructure Persistence Dependency Injection
builder.Services.AddInfrastructureServices(builder.Configuration);

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
    var adminUser = await db.Users.Select(u => new { u.UserId, u.Name, u.Email, u.IsActive }).FirstOrDefaultAsync();

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

app.Run();

