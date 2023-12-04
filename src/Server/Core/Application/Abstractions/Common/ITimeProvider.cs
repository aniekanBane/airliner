using NodaTime;

namespace Application.Abstractions.Common;

public interface ITimeProvider
{
    DateTime Now { get; }

    DateTime UtcNow { get; }

    DateTimeOffset OffsetNow { get; }

    DateTimeOffset OffsetUtcNow { get; }

    Instant Instant { get; }
}
