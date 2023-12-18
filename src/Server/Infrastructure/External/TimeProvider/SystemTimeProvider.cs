using Application.Abstractions.Common;
using NodaTime;

namespace External.TimeProvider;

internal sealed class SystemTimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
    public DateTimeOffset OffsetNow => DateTimeOffset.Now;
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
    public Instant Instant => SystemClock.Instance.GetCurrentInstant();
}
