using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// Granular sewing or assembly line on the production floor
/// Lowest organizational level for work orders, IoT and real-time tracking
/// </summary>
public class ProductionLine : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public Guid SectionId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int LineNumber { get; set; } = 1;
    public int TotalWorkstations { get; set; } = 30;
    
    // Zero-Float Law: Decimal required for efficiency percentages
    public decimal TargetEfficiencyPercentage { get; set; } = 65.00m;
    public int DailyCapacityMinutes { get; set; } = 480;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public Section? Section { get; set; }
}
