using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// Floor level within a building
/// </summary>
public class Floor : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public Guid BuildingId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int FloorNumber { get; set; } = 1;

    // Navigation Properties
    public Building? Building { get; set; }
    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
