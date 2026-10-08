namespace ThreadFlow.Domain.Common;

/// <summary>
/// Marks entities that support soft deletion
/// </summary>
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
    DateTimeOffset? DeletedAt { get; set; }
}
