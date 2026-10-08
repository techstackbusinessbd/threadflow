namespace ThreadFlow.Application.Common.Interfaces;

/// <summary>
/// Scoped service providing claims-based current user context for forensic audit logging
/// </summary>
public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
}
