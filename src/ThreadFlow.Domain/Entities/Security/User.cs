using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Entities.Tenancy;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// Application user synchronized with Keycloak Identity Provider
/// Carries tenant membership and user metadata
/// </summary>
public class User : AuditableEntity, ITenantScopedEntity
{
    public Guid TenantId { get; set; }
    public string KeycloakUserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public Tenant? Tenant { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<UserDataScope> DataScopes { get; set; } = new List<UserDataScope>();
}
