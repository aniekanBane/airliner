using Domain.Primitives.Common;
using Microsoft.Extensions.Logging;

namespace Persistence.Services.Logging;

public sealed class LoggerAdapter<T>(ILogger<T> logger) : IAppLogger<T>
{
    private readonly ILogger<T> _logger = logger;

    public void LogError(Exception? ex, string message, params object[] args)
    {
        _logger.LogError(ex, message, args);
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }
}
