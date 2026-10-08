using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// Operational production section on a floor (e.g. Cutting, Sewing, Finishing, Quality)
/// </summary>
public class Section : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public Guid FloorId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public SectionType Type { get; set; } = SectionType.Sewing;

    // Navigation Properties
    public Floor? Floor { get; set; }
    public ICollection<ProductionLine> ProductionLines { get; set; } = new List<ProductionLine>();
}
