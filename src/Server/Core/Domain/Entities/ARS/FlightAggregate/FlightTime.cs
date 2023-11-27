using Domain.Exceptions;

namespace Domain.Entities.ARS.FlightAggregate;

public readonly record struct FlightTime
{
    private FlightTime(Instant instant) => Instant = instant;

    public Instant Instant { get; }

    public static implicit operator Instant(FlightTime flightTime) => flightTime.Instant;

    public static implicit operator DateTime(FlightTime flightTime) 
        => flightTime.Instant.ToDateTimeUtc();

    public static explicit operator FlightTime(Instant instant) => FromInstant(instant);

    public static explicit operator FlightTime(DateTime dateTime) => FromDateTime(dateTime);

    public static FlightTime operator +(FlightTime flightTime, FlightDuration duration)
    {
        return (FlightTime)(flightTime.Instant + duration.Duration);
    }

    public static FlightTime FromInstant(Instant instant)
    {
        if (instant == default)
            throw new DomainException("Value ({value}) cannot be default", nameof(instant));

        return new FlightTime(instant);
    }

    public static FlightTime FromDateTime(DateTime dateTime)
    {
        if (dateTime == default)
            throw new DomainException("Value ({value}) cannot be default", nameof(dateTime));

        return new FlightTime(Instant.FromDateTimeUtc(dateTime));
    }
}
