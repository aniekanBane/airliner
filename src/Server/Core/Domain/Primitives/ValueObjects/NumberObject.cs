using Domain.Exceptions;

namespace Domain.Primitives.ValueObjects;

public abstract record class NumberObject<T> where T : NumberObject<T>
{
    protected internal NumberObject(int value) => Value = value;

    protected internal NumberObject(double value) => Value = value;

    protected internal NumberObject(decimal value) => Value = value;

    public object Value { get; }

    public static implicit operator int(NumberObject<T> number) => (int)number.Value;
    public static implicit operator double(NumberObject<T> number) => (double)number.Value;
    public static implicit operator decimal(NumberObject<T> number) => (decimal)number.Value;
    
    public static explicit operator NumberObject<T>(int value) => FromInt(value);
    public static explicit operator NumberObject<T>(double value) => FromDouble(value);
    public static explicit operator NumberObject<T>(decimal value) => FromDecimal(value);

    public static T FromInt(int value)
    {
        DomainException.ThrowIfLessThanOrEqual(value, 0, typeof(T).Name);
        return (T)Activator.CreateInstance(typeof(T), value)!;
    }

    public static T FromDouble(double value)
    {
        DomainException.ThrowIfLessThanOrEqual(value, 0.0, typeof(T).Name);
        return (T)Activator.CreateInstance(typeof(T), value)!;
    }

    public static T FromDecimal(decimal value)
    {
        DomainException.ThrowIfLessThanOrEqual(value, decimal.Zero, typeof(T).Name);
        return (T)Activator.CreateInstance(typeof(T), value)!;
    }
}
