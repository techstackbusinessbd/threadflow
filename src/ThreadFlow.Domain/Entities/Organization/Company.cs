using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Entities.Tenancy;

namespace ThreadFlow.Domain.Entities.Organization;

/// <summary>
/// Legal entity / Company within a conglomerate group
/// Enforces multi-tenant scoping and holds base accounting currency
/// </summary>
public class Company : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? RegistrationNumber { get; set; }
    public string? TaxIdentificationNumber { get; set; }
    public string BaseCurrencyCode { get; set; } = "USD";
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public Tenant? Tenant { get; set; }
    public ICollection<BusinessUnit> BusinessUnits { get; set; } = new List<BusinessUnit>();
    public ICollection<Factory> Factories { get; set; } = new List<Factory>();
}
