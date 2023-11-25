using Domain.Exceptions;

namespace Domain.Primitives.ValueObjects;

public abstract record class StringObject<T> where T : StringObject<T>
{
    protected internal StringObject(string value) => Value = value;

    public string Value { get; }

    public static implicit operator string(StringObject<T> stringValue) 
        => stringValue.Value;

    public static explicit operator StringObject<T>(string? value) => From(value);

    public static T From(string? value)
    {
        DomainException.ThrowIfNullOrWhiteSpace(value, typeof(T).Name);

        return (T)Activator.CreateInstance(typeof(T), value)!;
    }
}
