using Domain.Primitives.Common;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.DestinationAggregate;

public sealed class Airport : AuditableEntity<Guid>
{
    private readonly List<string> _terminals = [];
    private readonly string _iataCode;
    private readonly string _icaoCode;
    private readonly string _timeZone;

    private Airport(
        bool isHub,
        string iataCode, 
        string icaoCode, 
        string name, 
        string city,
        string country, 
        string timeZone)
    {
        _iataCode = iataCode;
        _icaoCode = icaoCode;
        _timeZone = timeZone;

        Name = name;
        City = city;
        IsHub = isHub;
        Country = country;
    }

    public string IataCode => _iataCode;

    public string IcaoCode => _icaoCode;

    public string Name { get; private set; }

    public string City { get; private set; }

    public string Country { get; private set; }

    public bool IsHub { get; private set; }

    public string TimeZone => _timeZone;

    public string CityId { get; private set; } = default!;

    public IList<string> Terminals => _terminals.AsReadOnly();

    public static Airport Create(
        bool isHub,
        string iataCode, 
        string icaoCode, 
        string name, 
        string city, 
        string country,
        string timeZone,
        ImmutableArray<string> terminals)
    {
        var airport = new Airport(
            isHub,
            (IATACode)iataCode, 
            (ICAOCode)icaoCode, 
            (AirportName)name, 
            (CityName)city, 
            (CountryName)country,
            (TimeZone)timeZone
        );

        foreach(var terminal in terminals)
            airport.AddTerminal(terminal);

        return airport;
    }

    public void AddTerminal(string terminal)
    {
        if (Terminals.Any(x => x.Equals(terminal, StringComparison.OrdinalIgnoreCase)))
            return;

        _terminals.Add((Terminal)terminal);
    }

    public bool RemoveTerminal(string terminal)
    {
        return _terminals.Remove((Terminal)terminal);
    }
}

public sealed record class AirportName(string Value) : StringObject<AirportName>(Value);

public sealed record class Terminal(string Value) : StringObject<Terminal>(Value);
