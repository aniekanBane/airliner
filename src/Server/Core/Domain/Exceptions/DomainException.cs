using System.Globalization;
using System.Runtime.CompilerServices;

namespace Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException() : base() {}

    public DomainException(string message) : base(message) {}

    public DomainException(string message, Exception? innerException) 
        : base(message, innerException) {}

    public DomainException(string message, params object?[] args) 
        : base(string.Format(CultureInfo.CurrentCulture, message, args)) {}

    public static void ThrowIfLessThanOrEqual(
        int value, int thres,
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (value <= thres)
            throw new DomainException("Value ({0}) cannot be less than {1}.", paramName, thres);
    }

    public static void ThrowIfLessThanOrEqual(
        double value, double thres,
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (value <= thres)
            throw new DomainException("Value ({0}) cannot be less than {1}.", paramName, thres);
    }

    public static void ThrowIfLessThanOrEqual(
        decimal value, decimal thres,
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (value <= thres)
            throw new DomainException("Value ({0}) cannot be less than {1}.", paramName, thres);
    }

    public static void ThrowIfOutOfRange(
        int value, int min, int max, 
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (value < min || value > max)
            throw new DomainException("Value ({0}) not within range.", paramName);
    }

    public static void ThrowIfNotEqual(
        int value, int thresh, 
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (value != thresh)
            throw new DomainException("Value ({0}) should be {1}.", paramName, thresh);
    }

    public static void ThrowIfNull(
        object? value, 
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (value is null)
            throw new DomainException("Value ({0}) cannot be null.", paramName);
    }

    public static void ThrowIfNullOrEmpty(
        string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (string.IsNullOrEmpty(value))
            throw new DomainException("Value ({0}) cannot be null or empty.", paramName);
    }

    public static void ThrowIfNullOrWhiteSpace(
        string? value,
        [CallerArgumentExpression(nameof(value))] string? paramName = default)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(
                "Value ({0}) cannot be null, enmpty or whitespace.", 
                paramName
            );
    }
}
