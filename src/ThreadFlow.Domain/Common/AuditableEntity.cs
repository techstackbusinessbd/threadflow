namespace ThreadFlow.Domain.Common;

/// <summary>
/// Base class for all auditable domain entities with UUIDv7, timestamps,
/// user forensics, soft delete, concurrency tracking, and IsDemo test isolation.
/// </summary>
public abstract class AuditableEntity : BaseEntity, ISoftDelete
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public string CreatedBy { get; set; } = "SYSTEM";
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    // Soft Deletion
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    // Demo / Test Data Isolation Flag
    public bool IsDemo { get; set; }

    // Concurrency Token (mapped to PostgreSQL xmin in EF Core)
    public uint Version { get; set; }
}
