using System.Collections.Immutable;
using Application.Abstractions.Database;
using Domain.Entities.AMS.Aircrafts;
using Domain.Entities.AMS.AircraftTypes;

namespace Persistence.Services.Database;

internal sealed class AMSDbContextSeeder(
    AMSDbContext dbContext
    /*IAppLogger<AMSDbContext> logger*/) : IDatabaseSeeder
{
    private readonly AMSDbContext _dbContext = dbContext;
    //private readonly IAppLogger<AMSDbContext> _logger = logger;

    private readonly ImmutableArray<AircraftType> aircraftTypes = [
        AircraftType.Create("CRJ-900", "Bombardier"),
        AircraftType.Create("A220-300", "Airbus")
    ];

    private readonly ImmutableArray<Aircraft> aircrafts = [
        Aircraft.Create(90, "Ikot", "CRJ-900", "5N-BWR", AircraftStatus.Pending),
        Aircraft.Create(90, "Ika", "CRJ-900", "5N-BWK", AircraftStatus.Pending),
        Aircraft.Create(90, "Abak", "CRJ-900", "5N-BWL", AircraftStatus.Pending),
        Aircraft.Create(150, "Abasi-Idiong", "A220-300", "SU-GFA", AircraftStatus.Pending)
    ];

    private readonly ImmutableArray<ImmutableArray<Cabin>> cabins = [
        [ Cabin.Create(14, CabinClass.Premium), Cabin.Create(76, CabinClass.Economy) ],
        [ Cabin.Create(14, CabinClass.Premium), Cabin.Create(76, CabinClass.Economy) ],
        [ Cabin.Create(14, CabinClass.Premium), Cabin.Create(76, CabinClass.Economy) ],
        [ Cabin.Create(15, CabinClass.Premium), Cabin.Create(135, CabinClass.Economy) ]
    ];

    public void Initialize()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        using var tx = _dbContext.Database.BeginTransaction();

        SeedAircrafts();

        _dbContext.SaveChanges();

        tx.Commit();
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await Task.Run(Initialize, cancellationToken);
    }

    private void SeedAircrafts()
    {
        if (_dbContext.AircraftTypes.Any())
        {
            ///_logger.LogInformation("skipping ...");
            return;
        }

        _dbContext.AircraftTypes.AddRange(aircraftTypes);
        //_logger.LogInformation("seeded aircraft types...");

        if (_dbContext.Aircrafts.Any())
        {
            return;
        }

        foreach(var (aircraft, cabins) in Enumerable.Zip(aircrafts, cabins))
        {
            foreach(var cabin in cabins)
            {
                aircraft.AddCabin(cabin.Capacity, cabin.CabinClass);
            }
        }

        _dbContext.Aircrafts.AddRange(aircrafts);
        //_logger.LogInformation("seeded aircrafts...");
    }
}
