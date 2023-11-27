using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.DestinationAggregate;

public sealed record class TimeZone(string Value) : StringObject<TimeZone>(Value)
{
    public static explicit operator TimeZone(string? value) => From(value);

    public new static TimeZone From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, nameof(TimeZone));

        if (!DateTimeZoneProviders.Tzdb.Ids.Any(x => x == value))
            throw new DomainException("Unrecognized time zone");

        return new TimeZone(value!);
    }
}
