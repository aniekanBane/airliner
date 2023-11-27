using Domain.Entities.AMS.Aircrafts;
using Domain.Entities.ARS.DestinationAggregate;
using Domain.Exceptions;
using Domain.Primitives.Common;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.FlightAggregate;

public sealed class Flight : AuditableEntity<Guid>, IAggregateRoot
{
    private readonly List<Fare> _fares = [];
    private readonly int _capacity;

    private Flight(
        int capacity,
        string aircraftId,
        string flightNumber,
        string departureAirport,
        string arrivalAirport,
        Instant departureTime,
        Duration duration,
        FlightType flightType)
    {
        _capacity = capacity;
        
        AircraftId = aircraftId;
        FlightNumber = flightNumber;
        AvailableSeats = capacity;
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        FlightType = flightType;
        Duration = duration;
        DepartureTime = departureTime;
        ArrivalTime = departureTime + duration;
        FlightStatus = FlightStatus.Pending;
    }

    public string FlightNumber { get; private set; }

    public string DepartureAirport { get; private set; }

    public string ArrivalAirport { get; private set; }

    public FlightType FlightType { get; private set; }

    public FlightStatus FlightStatus { get; private set; }

    public Instant DepartureTime { get; private set; }

    public Instant ArrivalTime { get; private set; }

    public Duration Duration { get; private set; }

    public Duration Delay { get; private set; }

    public int Capacity => _capacity;

    public int AvailableSeats { get; private set; }

    public string AircraftId { get; private set; }

    public Guid? FlightRouteId { get; private set; }

    public IReadOnlyCollection<Fare> Fares => _fares.AsReadOnly();

    public bool IsFullyBooked => AvailableSeats == 0;

    public bool IsCancelled => FlightStatus == FlightStatus.Cancelled;

    public static Flight Create(
        int flightCapacity,
        string? aircraftId, 
        string flightNumber, 
        string departureAirport, 
        string arrivalAirport,
        Instant departureTime, 
        Duration duration,
        FlightType flightType)
    {
        if (duration < Duration.FromMinutes(5))
            throw new DomainException("Value ({0}) cannot be less than 5 minutes.", nameof(duration));

        var flight = new Flight(
            (AircraftCapacity) flightCapacity,
            (AircraftId)aircraftId,
            (FlightNumber)flightNumber,
            (IATACode)departureAirport,
            (IATACode)arrivalAirport,
            (FlightTime)departureTime,
            (FlightDuration)duration,
            flightType
        );

        return flight;
    }

    public void CancelFlight()
    {
        if (!IsCancelled || !CanCancel())
            return;

        FlightStatus = FlightStatus.Cancelled;
    }

    public void DelayFlight(Duration duration)
    {
        if (!IsCancelled || !CanCancel())
            return;

        if (duration < Duration.FromMinutes(1))
            throw new DomainException("Delay cannot be less than a minute.");

        Delay += duration;
        DepartureTime += duration;
        ArrivalTime += duration;
        
        FlightStatus = FlightStatus.Delayed;
    }

    public void RescheduledFlight(Instant departure)
    {
        if (!IsCancelled || !CanCancel())
            return;

        if (departure < DepartureTime)
            throw new DomainException();

        DepartureTime = departure;
        ArrivalTime = departure + Duration;

        FlightStatus = FlightStatus.Rescheduled;
    }

    public void AddFare(
        decimal price,
        string name, 
        CabinClass cabinClass, 
        ImmutableArray<string> fareRules,
        int? allocatedSeats = default)
    {
        if (IsCancelled || IsFullyBooked || !CanCancel())
            return;

        if (allocatedSeats > AvailableSeats)
            return;

        var fare = Fare.Create(price, name, cabinClass, fareRules, allocatedSeats);

        //TODO: Created event

        _fares.Add(fare);
    }

    private bool CanCancel() => 
        FlightStatus 
        is not FlightStatus.OnRoute 
        or FlightStatus.Departed 
        or FlightStatus.Arrived;
}

public sealed record class AircraftId(string Value) : StringObject<AircraftId>(Value);

public sealed record class AircraftCapacity : NumberObject<AircraftCapacity>
{
    public AircraftCapacity(int value) : base(value) {}
}
