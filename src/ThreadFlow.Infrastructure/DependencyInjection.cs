using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThreadFlow.Application.Common.Interfaces;
using ThreadFlow.Infrastructure.Persistence;
using ThreadFlow.Infrastructure.Persistence.Interceptors;
using ThreadFlow.Infrastructure.Services;

namespace ThreadFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 1. Scoped Context Services
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // 2. EF Core SaveChanges Interceptors
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<TenantInterceptor>();

        // 3. PostgreSQL Database Registration
        var connectionString = configuration.GetConnectionString("DefaultConnection") 
            ?? "Host=localhost;Port=5433;Database=threadflow_db;Username=threadflow_admin;Password=ThreadFlowDevSecure2026!";

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(
                sp.GetRequiredService<AuditableEntityInterceptor>(),
                sp.GetRequiredService<TenantInterceptor>());

            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                npgsqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
            });
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
