using System.ComponentModel.DataAnnotations.Schema;
using Domain.Primitives.Events;

namespace Domain.Primitives.Common;

public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>>
{
    private readonly List<IDomainEvent> _domainEvents = [];

    [Column(Order = 0)]
    public TId Id { get; protected set; } = default!;

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public bool Equals(Entity<TId>? other)
    {
         if (other is null || GetType() != other.GetType())
            return false;

        return EqualityComparer<TId>.Default.Equals(other.Id, Id);
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || Equals(obj as Entity<TId>);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return Id?.GetHashCode() ^ 31 ?? base.GetHashCode();
    }

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}

public interface IEntity<TId> : IEntity
{
    TId Id { get; }
}

public interface IEntity
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    
    void ClearDomainEvents();
}
