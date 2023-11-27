using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.DestinationAggregate;

public sealed record class ICAOCode(string Value) : StringObject<ICAOCode>(Value)
{
    private const int lenght = 4;

    public static explicit operator ICAOCode(string? value) => From(value);

    public new static ICAOCode From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, nameof(ICAOCode));
        DomainException.ThrowIfNotEqual(value!.Length, lenght, nameof(ICAOCode.lenght));
        
        return new ICAOCode(value);
    }
}
