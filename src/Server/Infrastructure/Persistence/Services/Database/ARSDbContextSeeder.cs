using Application.Abstractions.Database;
using Domain.Entities.AMS.Aircrafts;
using Domain.Entities.ARS.DestinationAggregate;
using Domain.Entities.ARS.FlightAggregate;
using Domain.Entities.ARS.FlightRoute;
using NodaTime.Extensions;
using Airport = (string iata, string icao, string name, string timeZone, string[] terminals);

namespace Persistence.Services.Database;

internal sealed class ARSDbContextSeeder(
    ARSDbContext dbContext,
    AMSDbContext amsDbContext
    /*IAppLogger<ARSDbContextSeeder> logger*/) : IDatabaseSeeder
{
    private readonly ARSDbContext _dbContext = dbContext;
    private readonly AMSDbContext _amsDbContext = amsDbContext;
    //private readonly IAppLogger<ARSDbContextSeeder> _logger = logger;

    private readonly ImmutableArray<City> cities = [
        City.Create("Uyo", "Nigeria", string.Empty, []),
        City.Create("Abuja", "Nigeria", string.Empty, []),
        City.Create("Lagos", "Nigeria", string.Empty, []),
        City.Create("Port Harcourt", "Nigeria", string.Empty, []),
        City.Create("Accra", "Ghana", string.Empty, [])
    ];

    private readonly ImmutableArray<Airport> airports = [
        ("QUO", "DNAI", "Victor Attah International Airport", "Africa/Lagos", ["1"]),
        ("ABV", "DNAA", "Nnamdi Azikiwe International Airport", "Africa/Lagos", ["A"]),
        ("LOS", "DNMM", "Murtala Mohammed Airport", "Africa/Lagos", ["2", "I"]),
        ("PHC", "DNPO", "Port Harcourt International Airport", "Africa/Lagos", []),
        ("ACC", "DGAA", "Kotoka International Airport", "Africa/Accra", ["3"]),
    ];

    private readonly ImmutableArray<FlightRoute> flightRoutes = [
        FlightRoute.Create("QUO", "ABV"),
        FlightRoute.Create("ABV", "QUO"),
        FlightRoute.Create("QUO", "LOS"),
        FlightRoute.Create("LOS", "QUO"),
        FlightRoute.Create("LOS", "ABV"),
        FlightRoute.Create("ABV", "LOS"),
        FlightRoute.Create("LOS", "ACC"),
        FlightRoute.Create("ACC", "LOS")
    ];

    private readonly ImmutableArray<FlightSeeder> flights = [
        new(
            "5N-BWR", "QUO", "ABV", FlightType.Local, 
            TimeSpan.FromMinutes(65), [1, 2, 3, 4 , 5], 
            [("QI5020", new(7, 0)), ("QI5022", new(11, 0))],
            [ .. economyFares[0..3], .. premiumFares[0..2] ]
        ),

        new(
            "5N-BWR", "ABV", "QUO", FlightType.Local, 
            TimeSpan.FromMinutes(65), [1, 2, 3, 4, 5], 
            [("QI5021", new(9, 0)), ("QI5023", new(16, 30))],
            [ .. economyFares[0..3], .. premiumFares[0..2] ]
        ),

        new(
            "5N-BWL", "QUO", "LOS", FlightType.Local, 
            TimeSpan.FromMinutes(65), [1, 2, 3, 4, 5], 
            [("QI5010", new(6, 30))],
            [ .. economyFares[0..3], .. premiumFares[0..2] ]
        ),

        new(
            "5N-BWL", "LOS", "QUO", FlightType.Local, 
            TimeSpan.FromMinutes(65), [1, 2, 3, 4, 5], 
            [("QI5010", new(8, 30))],
            [ .. economyFares[0..3], .. premiumFares[0..2] ]
        ),

        new(
            "5N-BWK", "LOS", "ACC", FlightType.Regional, 
            TimeSpan.FromMinutes(60), [1, 2, 3, 4, 5], 
            [("QI6000", new(7, 0)), ("QI6002", new(16, 0))],
            [ .. economyFares[4..], .. premiumFares[3..] ]
        ),

        new(
            "5N-BWK", "ACC", "LOS", FlightType.Regional, 
            TimeSpan.FromMinutes(60), [1, 2, 3, 4, 5], 
            [("QI6001", new(8, 30)), ("QI6003", new(17, 30))],
            [ .. economyFares[4..], .. premiumFares[3..] ]
        ),
    ];

    private static readonly ImmutableArray<FareSeeder> economyFares = [
        new("Saver", CabinClass.Economy, 45000, FareTemplates.EconomyOptions.Saver),
        new("Lite", CabinClass.Economy, 60000, FareTemplates.EconomyOptions.Lite),
        new("Standard", CabinClass.Economy, 74000, FareTemplates.EconomyOptions.Standard),
        new("Flex", CabinClass.Economy, 88000, FareTemplates.EconomyOptions.Flex),

        new("Lite", CabinClass.Economy, 120000, FareTemplates.EconomyOptions.Lite),
        new("Standard", CabinClass.Economy, 177000, FareTemplates.EconomyOptions.Standard),
        new("Flex", CabinClass.Economy, 210000, FareTemplates.EconomyOptions.Flex),
    ];

    private static readonly ImmutableArray<FareSeeder> premiumFares = [
        new("Lite", CabinClass.Premium, 130000.75m, FareTemplates.PremiumOptions.Lite),
        new("Standard", CabinClass.Premium, 150000m, FareTemplates.PremiumOptions.Standard),
        new("Flex", CabinClass.Premium, 180000, FareTemplates.PremiumOptions.Flex),

        new("Lite", CabinClass.Premium, 343500, FareTemplates.PremiumOptions.Lite),
        new("Standard", CabinClass.Premium, 409780m, FareTemplates.PremiumOptions.Standard),
        new("Flex", CabinClass.Premium, 460550, FareTemplates.PremiumOptions.Flex),
    ];

    public void Initialize()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();

        using var tx = _dbContext.Database.BeginTransaction();

        SeedDestinations();

        SeedFlights();

        _dbContext.SaveChanges();

        tx.Commit();
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await Task.Run(Initialize, cancellationToken);
    }

    private void SeedDestinations()
    {
        if (_dbContext.Cities.Any())
        {
            //_logger.LogInformation("skipping {func} ...", nameof(SeedDestinations));
            return;
        }

        foreach(var (city, airport) in Enumerable.Zip(cities, airports))
        {
            city.AddAirport(
                airport.iata, 
                airport.icao, 
                airport.name, 
                airport.timeZone, 
                [.. airport.terminals],
                airport.iata == "QUO"
            );
        }

        _dbContext.Cities.AddRange(cities);
    }

    private void SeedFlights()
    {
        if (_dbContext.FlightRoutes.Any())
        {
            return;
        }

        _dbContext.FlightRoutes.AddRange(flightRoutes);

        foreach(var flight in flights)
        {
            var departureCode = flight.DepartureAirport;
            var arrivalCode = flight.ArrivalAirport;

            var aircraft = _amsDbContext.Aircrafts
                .FirstOrDefault(x => x.RegistrationNumber == flight.AircrafReg);

            if (aircraft is null) continue;

            var aircraftId = aircraft.Id;
            var capacity = aircraft.Capacity;
            var routeId = flightRoutes
                .FirstOrDefault(x => 
                x.DepartureAirportCode == departureCode 
                && x.ArrivalAirportCode == arrivalCode)?.Id;

            var StartDate = DateTime.UtcNow.Date.AddMonths(1);
            var endDate = StartDate.AddYears(1);
            var duration = flight.Duration.ToDuration();

            for (var date = StartDate; date <= endDate; date = date.AddDays(1))
            {
                if (!flight.Days.Contains((int)date.DayOfWeek)) continue;

                foreach (var(number, time) in flight.Times)
                {
                    var departureTime = date.Add(time.ToTimeSpan()).ToInstant();

                    var newFlight = Flight.Create(
                        capacity, 
                        aircraftId, 
                        number, 
                        departureCode, 
                        arrivalCode,
                        departureTime,
                        duration,
                        flight.FlightType
                    );

                    foreach (var fare in flight.Fares)
                    {
                        newFlight.AddFare(
                            fare.Price, 
                            fare.Name, 
                            fare.CabinClass, 
                            fare.Rules, 
                            fare.AvailableSeats
                        );
                    }

                    _dbContext.Add(newFlight);
                }
            }

        }
    }

    private readonly record struct FlightSeeder(
        string AircrafReg,
        string DepartureAirport,
        string ArrivalAirport,
        FlightType FlightType,
        TimeSpan Duration,
        ImmutableArray<int> Days,
        ImmutableArray<(string, TimeOnly)> Times,
        ImmutableArray<FareSeeder> Fares
    );

    private readonly record struct FareSeeder(
        string Name,
        CabinClass CabinClass,
        decimal Price,
        ImmutableArray<string> Rules,
        int? AvailableSeats = default
    );
}
