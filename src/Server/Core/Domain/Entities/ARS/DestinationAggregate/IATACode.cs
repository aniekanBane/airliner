using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.DestinationAggregate;

public sealed record class IATACode(string Value) : StringObject<IATACode>(Value)
{
    private const int lenght = 3;

    public static explicit operator IATACode(string? value) => From(value);

    public new static IATACode From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, nameof(IATACode));
        DomainException.ThrowIfNotEqual(value!.Length, lenght, nameof(IATACode));

        return new IATACode(value);
    }
}
