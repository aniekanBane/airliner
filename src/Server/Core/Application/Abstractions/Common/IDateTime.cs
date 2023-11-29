namespace Application.Abstractions.Common;

public interface ITimeProvider
{
    DateTime Now { get; }

    DateTime UtcNow { get; }
}
