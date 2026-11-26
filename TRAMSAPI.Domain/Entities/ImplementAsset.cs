namespace TRAMSAPI.Domain.Entities;

/// <summary>
/// Implements - Physical Asset
/// </summary>
public class ImplementAsset
{
    public long ImplementAssetId { get; set; }
    public long TenantId { get; set; }
    public long? ImplementMasterId { get; set; }
    public string ImplementCode { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string OwnershipType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public long CreatedBy { get; set; }
}
