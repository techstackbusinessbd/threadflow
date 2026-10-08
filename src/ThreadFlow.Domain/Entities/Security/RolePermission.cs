using ThreadFlow.Domain.Common;

namespace ThreadFlow.Domain.Entities.Security;

/// <summary>
/// Many-to-many join entity between Roles and Permissions
/// </summary>
public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }

    // Navigation Properties
    public Role? Role { get; set; }
    public Permission? Permission { get; set; }
}
