using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Application.Services;
using TRAMSAPI.Infrastructure;
using TRAMSAPI.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger with JWT Bearer support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TRAMSAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Bearer token only (without Bearer prefix)"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Infrastructure Persistence & Services Dependency Injection
builder.Services.AddInfrastructureServices(builder.Configuration);

// Application Services Dependency Injection
builder.Services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Configure JWT Authentication
var jwtSecret = builder.Configuration["JwtSettings:Secret"] ?? "TRAMS_SUPER_SECRET_JWT_SIGNING_KEY_2026_MIN_32_CHARS!";
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"] ?? "TRAMSAPI";
var jwtAudience = builder.Configuration["JwtSettings:Audience"] ?? "TRAMSAPI.Clients";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

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
