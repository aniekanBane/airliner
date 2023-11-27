using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.AMS.Aircrafts;

public sealed record class AircraftName(string Value) : StringObject<AircraftName>(Value)
{
    private const int minLength = 3;
    private const int maxLength = 64;

    public static explicit operator AircraftName(string? value) => From(value);

    public new static AircraftName From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, nameof(AircraftName));
        DomainException.ThrowIfOutOfRange(value!.Length, minLength, maxLength, nameof(AircraftName));

        return new AircraftName(value);
    }
}
