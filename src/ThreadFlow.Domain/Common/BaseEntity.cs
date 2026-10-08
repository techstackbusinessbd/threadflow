namespace ThreadFlow.Domain.Common;

/// <summary>
/// Root abstract base entity enforcing UUID primary keys (RFC 9562 UUIDv7)
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; }
}
