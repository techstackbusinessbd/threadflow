using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// Many-to-many join entity between Users and Roles
/// </summary>
public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    // Navigation Properties
    public User? User { get; set; }
    public Role? Role { get; set; }
}
