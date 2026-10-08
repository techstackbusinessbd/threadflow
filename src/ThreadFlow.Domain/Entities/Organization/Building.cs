using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// Physical building inside a factory compound
/// </summary>
public class Building : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public Guid FactoryId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int TotalFloors { get; set; } = 1;

    // Navigation Properties
    public Factory? Factory { get; set; }
    public ICollection<Floor> Floors { get; set; } = new List<Floor>();
}
