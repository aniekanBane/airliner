using MediatR;

namespace Domain.Primitives.Events;

public abstract class DomainEvent<TEntity> : IDomainEvent
{
    protected DomainEvent(TEntity entity)
    {
        Entity = entity;
        OccuredAt = DateTime.UtcNow;
    }

    public TEntity Entity { get; }

    public DateTime OccuredAt { get; }
}

public interface IDomainEvent : INotification
{

}
