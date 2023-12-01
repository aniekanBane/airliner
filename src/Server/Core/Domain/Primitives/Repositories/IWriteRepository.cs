using System.Linq.Expressions;

using Ardalis.Specification;
using Domain.Primitives.Common;

namespace Domain.Primitives.Repositories;

public interface IWriteRepository<TEntity> : IRepositoryBase<TEntity> 
    where TEntity : class, IEntity, IAggregateRoot
{
    public IUnitOfWork UnitOfWork { get; }
    
    Task<int> RemoveAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default
    );
}
