using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ThreadFlow.Application.Common.Interfaces;
using ThreadFlow.Domain.Common;

namespace ThreadFlow.Infrastructure.Persistence.Interceptors;

/// <summary>
/// EF Core SaveChangesInterceptor that automatically sets audit forensics (Created/Updated metadata)
/// and converts hard deletions into soft deletions for entities implementing ISoftDelete.
/// </summary>
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditableEntityInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditEntities(DbContext? context)
    {
        if (context == null) return;

        var now = DateTimeOffset.UtcNow;
        var currentActor = _currentUserService.Username ?? "SYSTEM";

        // 1. Process Auditable Entities
        foreach (var entry in context.ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                if (string.IsNullOrWhiteSpace(entry.Entity.CreatedBy) || entry.Entity.CreatedBy == "SYSTEM")
                {
                    entry.Entity.CreatedBy = currentActor;
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
                entry.Entity.UpdatedBy = currentActor;
            }
        }

        // 2. Intercept Hard Delete and convert to Soft Delete
        foreach (var entry in context.ChangeTracker.Entries<ISoftDelete>())
        {
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = now;
            }
        }
    }
}
