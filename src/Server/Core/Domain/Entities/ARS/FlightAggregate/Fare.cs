using Domain.Entities.AMS.Aircrafts;
using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.FlightAggregate;

public sealed record class Fare
{
    private readonly List<string> _fareRules = [];
    private int? _availableSeats;
    private decimal _price;

    private Fare(int? availableSeats, decimal price, string name, CabinClass cabinClass)
    {
        DomainException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        DomainException.ThrowIfOutOfRange(name.Length, 4, 24, nameof(name));
        DomainException.ThrowIfLessThanOrEqual(price, decimal.Zero, nameof(price));

        if (availableSeats is not null)
            DomainException.ThrowIfLessThanOrEqual(
                availableSeats.Value, 
                0, nameof(availableSeats)
            );

        _price = price;
        _availableSeats = availableSeats;

        Name = name;
        CabinClass = cabinClass;
    }

    public CabinClass CabinClass { get; init; }

    public decimal Price => _price;

    public string Name { get; init; }

    public int? AvailableSeats => _availableSeats;

    public IEnumerable<string> FareRules => _fareRules.AsReadOnly();

    public static Fare Create(
        decimal price, 
        string name, 
        CabinClass cabinClass,
        ImmutableArray<string> fareRules,
        int? seats = default)
    {
        var fare = new Fare(seats, price, name, cabinClass);
        
        foreach (var rule in fareRules)
            fare.AddFareRule(rule);

        return fare;
    }

    private void AddFareRule(string fareRule)
    {
        _fareRules.Add((FareRule)fareRule);
    }

    internal void Updateprice(decimal multiplier)
    {
        _price *= multiplier;
    }
}

public sealed record class FareRule(string Value) : StringObject<FareRule>(Value);