using MediatR;

namespace Domain.Primitives.Events;

public interface IDomainEventHandler<in TDomainevent> : INotificationHandler<TDomainevent> 
    where TDomainevent : IDomainEvent
{
}
