namespace ThreadFlow.Domain.Common;

/// <summary>
/// Marks entities belonging to a specific tenant in a SaaS-Ready multi-tenant architecture
/// Injected into EF Core global query filters to enforce strict data isolation
/// </summary>
public interface ITenantScopedEntity
{
    Guid TenantId { get; set; }
}
