using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TRAMSAPI.Application.Interfaces;
using TRAMSAPI.Application.Services;
using TRAMSAPI.Infrastructure;
using TRAMSAPI.Infrastructure.Persistence;
using TRAMSAPI.Web.Common;
using TRAMSAPI.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

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

// Application Services & Context Dependency Injection
builder.Services.AddScoped<ITenantRegistrationService, TenantRegistrationService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

// Configure Rate Limiting (Protects Login from brute force attacks)
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, token) =>
    {
        context.HttpContext.Response.ContentType = "application/json";
        await context.HttpContext.Response.WriteAsJsonAsync(new
        {
            statusCode = 429,
            error = "TooManyRequests",
            message = "Too many login attempts. Please wait 1 minute before trying again."
        }, cancellationToken: token);
    };

    options.AddPolicy("LoginLimiter", httpContext =>
    {
        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetFixedWindowLimiter(ipAddress, _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 5,
            Window = TimeSpan.FromMinutes(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });
    });
});

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

    // Standardized 401 Unauthorized & 403 Forbidden JSON formatting
    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var isExpired = context.AuthenticateFailure is SecurityTokenExpiredException;
            var response = new
            {
                statusCode = 401,
                error = isExpired ? "TokenExpired" : "Unauthorized",
                message = isExpired
                    ? "Your session has expired. Please log in again."
                    : "Access denied. A valid Bearer token is required to access this resource."
            };

            await context.Response.WriteAsJsonAsync(response);
        },
        OnForbidden = async context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var response = new
            {
                statusCode = 403,
                error = "Forbidden",
                message = "You do not have the required permissions to access this resource."
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// 1. Global Exception Middleware (at the very top to catch any unhandled crash)
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 2. Rate Limiting Middleware
app.UseRateLimiter();

// 3. Authentication Middleware
app.UseAuthentication();

// 4. Real-time Account Status Validation Middleware (Verifies active user & tenant)
app.UseMiddleware<ActiveAccountValidationMiddleware>();

// 5. Authorization Middleware
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
