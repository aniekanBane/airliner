using System.Text.RegularExpressions;

using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.FlightAggregate;

public sealed record class FlightNumber(string Value) : StringObject<FlightNumber>(Value)
{
    public static explicit operator FlightNumber(string? value) => From(value);

    public new static FlightNumber From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, nameof(FlightNumber));

        if (!RegularExpressions.FlightNoRegex().IsMatch(value!))
            throw new DomainException("Value ({0}) format is invalid", nameof(FlightNumber));

        return new FlightNumber(value!);
    }
}

public static partial class RegularExpressions
{
    [GeneratedRegex(@"QI\d{4}")]
    public static partial Regex FlightNoRegex();
}
