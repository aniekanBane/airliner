namespace Domain.Primitives.Events;

public abstract class DomainEvent<TEntity>(TEntity entity) : IDomainEvent
{
    public TEntity Entity { get; } = entity;

    public DateTime OccuredAt { get; } = DateTime.UtcNow;
}

public class CreatedEvent<TEntity>(TEntity entity) : DomainEvent<TEntity>(entity);

public class DeletedEvent<TEntity>(TEntity entity) : DomainEvent<TEntity>(entity);

public class UpdateEvent<TEntity>(TEntity entity) : DomainEvent<TEntity>(entity);
