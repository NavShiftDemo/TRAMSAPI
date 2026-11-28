using System.Net;
using System.Text.Json;

namespace TRAMSAPI.Web.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var traceId = context.TraceIdentifier;
            _logger.LogError(ex, ">>> [UNHANDLED EXCEPTION] TraceId: {TraceId} | Error: {Message}", traceId, ex.Message);

            await HandleExceptionAsync(context, traceId);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string traceId)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorResponse = new
        {
            statusCode = 500,
            error = "InternalServerError",
            message = "An unexpected error occurred while processing your request. Please contact support.",
            traceId = traceId
        };

        var json = JsonSerializer.Serialize(errorResponse);
        return context.Response.WriteAsync(json);
    }
}
