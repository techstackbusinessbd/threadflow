using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using ThreadFlow.Application.Common.Interfaces;
using ThreadFlow.Domain.Common;
using ThreadFlow.Domain.Entities.Organization;
using ThreadFlow.Domain.Entities.Security;
using ThreadFlow.Domain.Entities.Tenancy;

namespace ThreadFlow.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ITenantService _tenantService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ITenantService tenantService) : base(options)
    {
        _tenantService = tenantService;
    }

    // Tenancy
    public DbSet<Tenant> Tenants => Set<Tenant>();

    // Organizational 8-Tier Spatial Hierarchy
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<BusinessUnit> BusinessUnits => Set<BusinessUnit>();
    public DbSet<Factory> Factories => Set<Factory>();
    public DbSet<Building> Buildings => Set<Building>();
    public DbSet<Floor> Floors => Set<Floor>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<ProductionLine> ProductionLines => Set<ProductionLine>();

    // Security & Authorization
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<UserDataScope> UserDataScopes => Set<UserDataScope>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Configure Global Query Filters for Tenancy & Soft Deletion
        modelBuilder.Entity<Company>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<BusinessUnit>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<Factory>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<Building>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<Floor>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<Section>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<ProductionLine>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<User>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);
        modelBuilder.Entity<UserDataScope>().HasQueryFilter(e => !_tenantService.CurrentTenantId.HasValue || e.TenantId == _tenantService.CurrentTenantId.Value);

        // Global Soft Delete Filters
        modelBuilder.Entity<Tenant>().HasQueryFilter(e => !e.IsDeleted);

        // 2. Configure Unique Constraints & Indexing
        modelBuilder.Entity<Tenant>(b =>
        {
            b.HasIndex(x => x.TenantCode).IsUnique();
            b.HasIndex(x => x.Subdomain).IsUnique();
        });

        modelBuilder.Entity<Company>(b =>
        {
            b.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();
        });

        modelBuilder.Entity<Factory>(b =>
        {
            b.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();
        });

        modelBuilder.Entity<Building>(b =>
        {
            b.HasIndex(x => new { x.TenantId, x.FactoryId, x.Code }).IsUnique();
            b.Property(x => x.TotalAreaSqft).HasPrecision(12, 2);
        });

        modelBuilder.Entity<Floor>(b =>
        {
            b.HasIndex(x => new { x.TenantId, x.BuildingId, x.Code }).IsUnique();
            b.Property(x => x.UsableAreaSqft).HasPrecision(12, 2);
        });

        modelBuilder.Entity<Section>(b =>
        {
            b.HasIndex(x => new { x.TenantId, x.FloorId, x.Code }).IsUnique();
        });

        modelBuilder.Entity<ProductionLine>(b =>
        {
            b.HasIndex(x => new { x.TenantId, x.SectionId, x.Code }).IsUnique();
            b.Property(x => x.TargetEfficiencyPercentage).HasPrecision(5, 2);
        });

        modelBuilder.Entity<User>(b =>
        {
            b.HasIndex(x => new { x.TenantId, x.Email }).IsUnique();
            b.HasIndex(x => x.KeycloakUserId).IsUnique();
        });

        modelBuilder.Entity<Permission>(b =>
        {
            b.HasIndex(x => x.Code).IsUnique();
        });

        modelBuilder.Entity<RolePermission>(b =>
        {
            b.HasKey(x => new { x.RoleId, x.PermissionId });
        });

        modelBuilder.Entity<UserRole>(b =>
        {
            b.HasKey(x => new { x.UserId, x.RoleId });
        });

        // 3. PostgreSQL xmin Concurrency Token Mapping & Snake Case Naming
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Set snake_case table names
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
            {
                entity.SetTableName(ToSnakeCase(tableName));
            }

            // Set snake_case column names
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.Name));
            }

            // Map Concurrency Token for AuditableEntity
            if (typeof(AuditableEntity).IsAssignableFrom(entity.ClrType))
            {
                modelBuilder.Entity(entity.ClrType).Property(nameof(AuditableEntity.Version)).IsConcurrencyToken();
            }
        }
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var startUnderscores = Regex.Match(input, @"^_+");
        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}
