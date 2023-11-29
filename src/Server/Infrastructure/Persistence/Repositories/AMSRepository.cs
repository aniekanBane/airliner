using Domain.Primitives.Common;

namespace Persistence.Repositories;

internal sealed class AMSRepository<TEntity>(AMSDbContext dbContext) : Repository<TEntity>(dbContext)
    where TEntity : class, IEntity, IAggregateRoot
{
}
