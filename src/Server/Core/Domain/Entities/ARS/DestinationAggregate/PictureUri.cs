using Domain.Exceptions;
using Domain.Primitives.ValueObjects;

namespace Domain.Entities.ARS.DestinationAggregate;

public sealed record class PictureUri(string Value) : StringObject<PictureUri>(Value)
{
    private static readonly string[] ext = [ ".png", ".jpg", ".jpeg", ".webp" ];

    public static explicit operator PictureUri(string? value) => From(value);

    public new static PictureUri From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, nameof(PictureUri));

        if (!ext.Any(value!.EndsWith))
            throw new DomainException("Acceptable image extensions: ", ext);

        return new PictureUri(value);
    }
}
