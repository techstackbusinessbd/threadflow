using ThreadFlow.Application.Common.Interfaces;

namespace ThreadFlow.Infrastructure.Services;

/// <summary>
/// Scoped CurrentUser resolution service (populated from HttpContext claims)
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    public string? UserId { get; set; }
    public string? Username { get; set; } = "SYSTEM";
    public string? Email { get; set; }
    public bool IsAuthenticated => !string.IsNullOrEmpty(UserId);
}
