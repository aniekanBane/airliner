using Application.Abstractions.Database;
using Domain.Primitives.Repositories;
using Domain.Primitives.ValueObjects;
using Persistence.Interceptors;
using Persistence.Repositories;
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
        services.AddScoped<DomainEventInterceptor>();
        services.AddSingleton<DomainEventToOutboxMessageInterceptor>();

        services.AddDbContext<AMSDbContext>(options =>
        {
            options.UseNpgsql(AMSDbContext.DataSource(
                (ConnectionString)configuration.GetConnectionString("AMSConn"))
            );
        }).AddTransient<IDatabaseSeeder, AMSDbContextSeeder>();

        services.AddDbContext<ARSDbContext>(options => 
        {
            options.UseNpgsql(ARSDbContext.DataSource((
                ConnectionString)configuration.GetConnectionString("ARSConn")), 
                o =>
                {
                    o.UseNodaTime();
                }
            );
        }).AddTransient<IDatabaseSeeder, ARSDbContextSeeder>();

        services.AddKeyedScoped(typeof(IReadRepository<>), "ars", typeof(ARSRepository<>));
        services.AddKeyedScoped(typeof(IWriteRepository<>), "ars", typeof(ARSRepository<>));

        services.AddKeyedScoped(typeof(IReadRepository<>), "ams", typeof(AMSRepository<>));
        services.AddKeyedScoped(typeof(IWriteRepository<>), "ams", typeof(AMSRepository<>));

        return services;
    }
}

public sealed record class ConnectionString(string Value) : StringObject<ConnectionString>(Value);
