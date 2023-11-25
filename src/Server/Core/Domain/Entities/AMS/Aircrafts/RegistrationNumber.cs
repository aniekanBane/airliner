using System.Text.RegularExpressions;

using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.AMS.Aircrafts;

public sealed record class RegistrationNumber(string Value) 
    : StringObject<RegistrationNumber>(Value)
{
    public static explicit operator RegistrationNumber(string? value) => From(value);

    public new static RegistrationNumber From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, nameof(RegistrationNumber));

        if (!RegularExpressions.RegNumRegex().IsMatch(value!))
            throw new DomainException("Value ({0}) format is invalid.", nameof(RegistrationNumber));

        return new RegistrationNumber(value!);
    }
}

public static partial class RegularExpressions
{
    [GeneratedRegex(@"([A-Z0-9]{1,2})(-?)([0-9A-Z]{1,5})")]
    public static partial Regex RegNumRegex();
}
