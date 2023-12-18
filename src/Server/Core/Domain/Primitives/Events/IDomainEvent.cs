using MediatR;

namespace Domain.Primitives.Events;

public interface IDomainEvent : INotification
{
    DateTime OccuredAt { get; }
}
