using Domain.Entities.AMS.Aircrafts;
using Domain.Entities.AMS.AircraftTypes;
using Persistence.Configurations;
using Persistence.Interceptors;

using Npgsql;

namespace Persistence;

internal sealed class AMSDbContext(
    DbContextOptions<AMSDbContext> options,
    AuditSaveInterceptor auditSaveInterceptor,
    DomainEventToOutboxMessageInterceptor outboxMessageInterceptor
) : DbContextUnitOfWork<AMSDbContext>(options, auditSaveInterceptor, outboxMessageInterceptor)
{
    public DbSet<Aircraft> Aircrafts => Set<Aircraft>();
    public DbSet<AircraftType> AircraftTypes => Set<AircraftType>();

    public static NpgsqlDataSource DataSource(string conn)
    {
        var sourceBuiler = new NpgsqlDataSourceBuilder(conn);

        sourceBuiler.MapEnum<AircraftStatus>();
        sourceBuiler.MapEnum<CabinClass>();

        return sourceBuiler.Build();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AircraftConfiguration());
        modelBuilder.ApplyConfiguration(new AircraftTypeConfiguration());

        modelBuilder.HasPostgresEnum<CabinClass>();
        modelBuilder.HasPostgresEnum<AircraftStatus>();
    }
}
