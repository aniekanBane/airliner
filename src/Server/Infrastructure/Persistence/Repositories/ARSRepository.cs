using Domain.Primitives.Common;

namespace Persistence.Repositories;

internal sealed class ARSRepository<TEntity>(ARSDbContext dbContext) 
    : Repository<TEntity, ARSDbContext>(dbContext) 
    where TEntity : class, IEntity, IAggregateRoot
{
}
