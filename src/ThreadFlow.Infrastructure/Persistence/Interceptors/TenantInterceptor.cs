using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ThreadFlow.Application.Common.Interfaces;
using ThreadFlow.Domain.Common;

namespace ThreadFlow.Infrastructure.Persistence.Interceptors;

/// <summary>
/// EF Core SaveChangesInterceptor that automatically sets the active TenantId
/// on newly created entities implementing ITenantScopedEntity.
/// </summary>
public class TenantInterceptor : SaveChangesInterceptor
{
    private readonly ITenantService _tenantService;

    public TenantInterceptor(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SetTenantId(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        SetTenantId(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetTenantId(DbContext? context)
    {
        if (context == null) return;

        var currentTenantId = _tenantService.CurrentTenantId;
        if (!currentTenantId.HasValue) return;

        foreach (var entry in context.ChangeTracker.Entries<ITenantScopedEntity>())
        {
            if (entry.State == EntityState.Added && entry.Entity.TenantId == Guid.Empty)
            {
                entry.Entity.TenantId = currentTenantId.Value;
            }
        }
    }
}
