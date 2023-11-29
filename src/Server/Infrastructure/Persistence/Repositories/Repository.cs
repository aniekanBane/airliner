using System.Linq.Expressions;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Primitives.Common;
using Domain.Primitives.Repositories;

namespace Persistence.Repositories;

internal abstract class Repository<TEntity>
    : RepositoryBase<TEntity>, IReadRepository<TEntity>, IWriteRepository<TEntity>
    where TEntity : class, IEntity, IAggregateRoot
{
    protected readonly DbSet<TEntity> _dbset;

    protected Repository(DbContext dbContext) : base(dbContext)
    {
        _dbset = dbContext.Set<TEntity>();
    }

    public override async Task<TEntity> AddAsync(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        await _dbset.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default)
    {
        return await _dbset.AnyAsync(predicate, cancellationToken);
    }

    public Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<int> RemoveAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default)
    {
        return await _dbset.Where(predicate).ExecuteDeleteAsync(cancellationToken);
    }
}
