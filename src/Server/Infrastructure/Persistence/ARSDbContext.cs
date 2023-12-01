using Domain.Entities.AMS.Aircrafts;
using Domain.Entities.ARS.DestinationAggregate;
using Domain.Entities.ARS.FlightAggregate;
using Domain.Entities.ARS.FlightRoute;
using Persistence.Configurations;

using Npgsql;

namespace Persistence;

internal sealed class ARSDbContext(DbContextOptions<ARSDbContext> options) 
    : DbContextUnitOfWork<ARSDbContext>(options)
{
    public DbSet<City> Cities => Set<City>();
    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<FlightRoute> FlightRoutes => Set<FlightRoute>();

    public static NpgsqlDataSource DataSource(string conn)
    {
        var sourceBuiler = new NpgsqlDataSourceBuilder(conn);

        sourceBuiler.UseNodaTime();
        sourceBuiler.MapEnum<RouteStatus>();
        sourceBuiler.MapEnum<FlightStatus>();
        sourceBuiler.MapEnum<FlightType>();
        sourceBuiler.MapEnum<CabinClass>();

        return sourceBuiler.Build();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CityConfiguration());
        modelBuilder.ApplyConfiguration(new AirportConfiguration());
        modelBuilder.ApplyConfiguration(new FlightConfiguration());
        modelBuilder.ApplyConfiguration(new FlightRouteConfiguration());

        modelBuilder.HasPostgresEnum<RouteStatus>();
        modelBuilder.HasPostgresEnum<FlightStatus>();
        modelBuilder.HasPostgresEnum<FlightType>();
        modelBuilder.HasPostgresEnum<CabinClass>();
    }
}
