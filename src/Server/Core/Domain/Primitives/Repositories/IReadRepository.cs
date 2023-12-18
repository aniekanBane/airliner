using System.Linq.Expressions;

using Ardalis.Specification;
using Domain.Primitives.Common;

namespace Domain.Primitives.Repositories;

public interface IReadRepository<TEntity> : IReadRepositoryBase<TEntity>
    where TEntity : class, IEntity, IAggregateRoot
{
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default
    );

    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default
    );

    Task<TUnMapped?> SqlQueryAsync<TUnMapped>(
        FormattableString sql, 
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<TUnMapped>> SqlQueryListAsync<TUnMapped>(
        FormattableString sql, 
        CancellationToken cancellationToken = default
    );
}
