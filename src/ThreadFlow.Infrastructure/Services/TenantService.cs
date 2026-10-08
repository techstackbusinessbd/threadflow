using ThreadFlow.Application.Common.Interfaces;

namespace ThreadFlow.Infrastructure.Services;

/// <summary>
/// Scoped Tenant resolution service
/// </summary>
public class TenantService : ITenantService
{
    public Guid? CurrentTenantId { get; private set; }
    public string? CurrentTenantCode { get; private set; }

    public void SetTenant(Guid tenantId, string? tenantCode = null)
    {
        CurrentTenantId = tenantId;
        CurrentTenantCode = tenantCode;
    }
}
