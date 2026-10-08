using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// Operational Business Unit dividing specialized divisions (e.g., Apparel vs Spinning vs Dyeing)
/// </summary>
public class BusinessUnit : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public BusinessUnitType Type { get; set; } = BusinessUnitType.ApparelManufacturing;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public Company? Company { get; set; }
    public ICollection<Factory> Factories { get; set; } = new List<Factory>();
}
