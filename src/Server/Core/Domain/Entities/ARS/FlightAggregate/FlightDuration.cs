using Domain.Exceptions;

namespace Domain.Entities.ARS.FlightAggregate;

public readonly record struct FlightDuration
{
    private FlightDuration(Duration duration) => Duration = duration;

    public Duration Duration { get; }

    public static implicit operator Duration(FlightDuration duration) => duration.Duration;

    public static implicit operator TimeSpan(FlightDuration duration) 
        => duration.Duration.ToTimeSpan();

    public static explicit operator FlightDuration(Duration duration) => FromDuration(duration);

    public static explicit operator FlightDuration(TimeSpan timeSpan) => FromTimeSpan(timeSpan);

    public static FlightDuration FromDuration(Duration duration)
    {
        if (duration == default)
            throw new DomainException("Value ({value}) cannot be default", nameof(duration));

        return new FlightDuration(duration);
    }

    public static FlightDuration FromTimeSpan(TimeSpan timeSpan)
    {
        if (timeSpan == default)
            throw new DomainException("Value ({value}) cannot be default", nameof(timeSpan));

        return new FlightDuration(Duration.FromTimeSpan(timeSpan));
    }
}
