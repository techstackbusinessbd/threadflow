using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// Physical manufacturing factory unit adhering to specific industrial archetypes
/// (KNIT, WOVEN, DENIM, SWEATER, WASH, EMBELLISHMENT, COMPOSITE_MULTI)
/// </summary>
public class Factory : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid? BusinessUnitId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public FactoryArchetype Archetype { get; set; } = FactoryArchetype.KnitDedicated;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public Company? Company { get; set; }
    public BusinessUnit? BusinessUnit { get; set; }
    public ICollection<Building> Buildings { get; set; } = new List<Building>();
}
