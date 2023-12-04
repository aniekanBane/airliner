﻿using Application.Abstractions.Database;
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

        services.AddDbContext<AMSDbContext>((sp, options) =>
        {
            options.UseNpgsql(AMSDbContext.DataSource(
                (ConnectionString)configuration.GetConnectionString("AMSConn"))
            );

            var auditInt = sp.GetRequiredService<AuditSaveInterceptor>();
            var domainEventInt = sp.GetRequiredService<DomainEventInterceptor>();
            options.AddInterceptors(auditInt, domainEventInt);
        }).AddTransient<IDatabaseSeeder, AMSDbContextSeeder>();

        services.AddDbContext<ARSDbContext>((sp, options) => 
        {
            options.UseNpgsql(ARSDbContext.DataSource((
                ConnectionString)configuration.GetConnectionString("ARSConn")), 
                o =>
                {
                    o.UseNodaTime();
                }
            );

            var auditInt = sp.GetRequiredService<AuditSaveInterceptor>();
            var domainEventInt = sp.GetRequiredService<DomainEventInterceptor>();
            options.AddInterceptors(auditInt, domainEventInt);
        }).AddTransient<IDatabaseSeeder, ARSDbContextSeeder>();

        services.AddKeyedScoped(typeof(IReadRepository<>), "ars", typeof(ARSRepository<>));
        services.AddKeyedScoped(typeof(IWriteRepository<>), "ars", typeof(ARSRepository<>));

        services.AddKeyedScoped(typeof(IReadRepository<>), "ams", typeof(AMSRepository<>));
        services.AddKeyedScoped(typeof(IWriteRepository<>), "ams", typeof(AMSRepository<>));

        return services;
    }
}

public sealed record class ConnectionString(string Value) : StringObject<ConnectionString>(Value);
