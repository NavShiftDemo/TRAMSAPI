using TRAMSAPI.Domain.Enums;

namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Stores API errors and application exception logs.
/// </summary>
public class ErrorLog
{
    public long ErrorLogId { get; set; }
    public long? TenantId { get; set; }
    public long? UserId { get; set; }
    public string? RequestPath { get; set; }
    public HttpMethodType? HttpMethod { get; set; }
    public int? StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public string? ExceptionType { get; set; }
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
