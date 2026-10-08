namespace ThreadFlow.Application.Common.Interfaces;

/// <summary>
/// Scoped service resolving active tenant context from HTTP request (X-Tenant-Id header, subdomain, or JWT claim)
/// Injected into EF Core ApplicationDbContext to drive tenant-isolated global query filters
/// </summary>
public interface ITenantService
{
    Guid? CurrentTenantId { get; }
    string? CurrentTenantCode { get; }
    void SetTenant(Guid tenantId, string? tenantCode = null);
}
