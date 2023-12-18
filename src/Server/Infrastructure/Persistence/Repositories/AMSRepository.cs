using Domain.Primitives.Common;

namespace Persistence.Repositories;

internal sealed class AMSRepository<TEntity>(AMSDbContext dbContext) 
    : Repository<TEntity, AMSDbContext>(dbContext)
    where TEntity : class, IEntity, IAggregateRoot
{
}
