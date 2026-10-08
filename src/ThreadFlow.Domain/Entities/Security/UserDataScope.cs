using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// 8-Tier Spatial & Organizational Data Scope Entity
/// Enforces granular data access boundaries: Tenant -> Company -> Factory -> Building -> Floor -> Section -> Line
/// If a level is NULL, access cascades to all children under the specified parent
/// </summary>
public class UserDataScope : AuditableEntity, ITenantScopedEntity
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    
    // Spatial Hierarchy Filters (Null means unrestricted under parent)
    public Guid? CompanyId { get; set; }
    public Guid? FactoryId { get; set; }
    public Guid? BuildingId { get; set; }
    public Guid? FloorId { get; set; }
    public Guid? SectionId { get; set; }
    public Guid? ProductionLineId { get; set; }

    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public User? User { get; set; }
}
