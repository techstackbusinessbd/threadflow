using Microsoft.EntityFrameworkCore;
using ThreadFlow.Domain.Entities.Organization;
using ThreadFlow.Domain.Entities.Security;
using ThreadFlow.Domain.Entities.Tenancy;

namespace ThreadFlow.Application.Common.Interfaces;

/// <summary>
/// Primary EF Core DbContext interface exposed to CQRS commands & queries
/// </summary>
public interface IApplicationDbContext
{
    // Tenancy
    DbSet<Tenant> Tenants { get; }

    // Organizational 8-Tier Spatial Hierarchy
    DbSet<Company> Companies { get; }
    DbSet<BusinessUnit> BusinessUnits { get; }
    DbSet<Factory> Factories { get; }
    DbSet<Building> Buildings { get; }
    DbSet<Floor> Floors { get; }
    DbSet<Section> Sections { get; }
    DbSet<ProductionLine> ProductionLines { get; }

    // Security & Authorization
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<UserRole> UserRoles { get; }
    DbSet<UserDataScope> UserDataScopes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
