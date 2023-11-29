using Application.Abstractions.Common;

namespace Persistence.Services.Common;

public sealed class SystemTimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
}
