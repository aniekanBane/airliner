using Domain.Primitives.Common;

namespace Persistence.Models.Messaging;

public sealed class OutboxMessage(
    string eventType, 
    string content, 
    DateTime occurredAtUtc) : Entity<Guid>, IAggregateRoot
{
    public string EventType { get; private set; } = eventType;

    public string Content { get; private set; } = content;

    public DateTime OccurredAtUtc { get; private set; } = occurredAtUtc;

    public DateTime ProcessedOnUtc { get; private set; }

    public bool IsPublished { get; private set; } = false;

    public string? Error { get; private set; }

    public void PublishMessage(DateTime processedTimeUtc)
    {
        ProcessedOnUtc = processedTimeUtc;
        IsPublished = true;
    }

    public void AddError(string error)
    {
        Error = error;
    }
}
