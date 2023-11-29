using Domain.Primitives.Common;

namespace Persistence.Repositories;

internal sealed class ARSRepository<TEntity>(ARSDbContext dbContext) 
    : Repository<TEntity>(dbContext) where TEntity : class, IEntity, IAggregateRoot
{
}
