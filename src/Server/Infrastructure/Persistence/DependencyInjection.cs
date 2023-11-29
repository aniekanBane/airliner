using Application.Abstractions.Common;
using Application.Abstractions.Database;
using Domain.Primitives.Repositories;
using Domain.Primitives.ValueObjects;
using Persistence.Interceptors;
using Persistence.Repositories;
using Persistence.Services.Common;
using Persistence.Services.Database;

using Microsoft.Extensions.Configuration;

using Npgsql;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<AuditSaveInterceptor>();

        services.AddDbContext<AMSDbContext>((sp, options) =>
        {
            options.UseNpgsql(AMSDbContext
                .AMSSource((ConnectionString)configuration.GetConnectionString("AMSConn")));

            var auditInt = sp.GetRequiredService<AuditSaveInterceptor>();
            options.AddInterceptors(auditInt);
        }).AddTransient<IDatabaseSeeder, AMSDbContextSeeder>();

        services.AddDbContext<ARSDbContext>((sp, options) => 
        {
            options.UseNpgsql(ARSDbContext
                .ARSSource((ConnectionString)configuration.GetConnectionString("ARSConn")), o =>
                {
                    o.UseNodaTime();
                });

            var auditInt = sp.GetRequiredService<AuditSaveInterceptor>();
            options.AddInterceptors(auditInt);
        }).AddTransient<IDatabaseSeeder, ARSDbContextSeeder>();

        services.AddKeyedScoped(typeof(IReadRepository<>), "ars", typeof(ARSRepository<>));
        services.AddKeyedScoped(typeof(IWriteRepository<>), "ars", typeof(ARSRepository<>));

        services.AddKeyedScoped(typeof(IReadRepository<>), "ams", typeof(AMSRepository<>));
        services.AddKeyedScoped(typeof(IWriteRepository<>), "ams", typeof(AMSRepository<>));

        services.AddTransient<ITimeProvider, SystemTimeProvider>();

        return services;
    }
}

public sealed record class ConnectionString(string Value) : StringObject<ConnectionString>(Value);
