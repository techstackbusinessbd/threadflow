using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Entities.Organization;
using ThreadFlow.Domain.Enums;

namespace ThreadFlow.Domain.Entities.Tenancy;

/// <summary>
/// Root Tenant Entity for SaaS-Ready Hybrid Multi-Tenancy Architecture
/// Acts as the top-level isolation container for all organizational, security, and transaction data
/// </summary>
public class Tenant : AuditableEntity
{
    public string TenantCode { get; set; } = string.Empty;
    public string TenantName { get; set; } = string.Empty;
    public string Subdomain { get; set; } = string.Empty;
    public SubscriptionTier SubscriptionTier { get; set; } = SubscriptionTier.Professional;
    public string AdminEmail { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTimeOffset? SubscriptionExpiresAt { get; set; }

    // Navigation Collections
    public ICollection<Company> Companies { get; set; } = new List<Company>();
}
