using System.Linq.Expressions;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Primitives.Common;
using Domain.Primitives.Repositories;

namespace Persistence.Repositories;

internal abstract class Repository<TEntity, TDbContext>(TDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext), IReadRepository<TEntity>, IWriteRepository<TEntity>
    where TEntity : class, IEntity, IAggregateRoot
    where TDbContext : DbContext, IUnitOfWork
{
    protected readonly DbSet<TEntity> _dbset = dbContext.Set<TEntity>();

    public IUnitOfWork UnitOfWork { get; } = dbContext;

    public override async Task<TEntity> AddAsync(
        TEntity entity, 
        CancellationToken cancellationToken = default)
    {
        await _dbset.AddAsync(entity, cancellationToken);
        return entity;
    }

    public override Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
       _dbset.Update(entity);
        return Task.CompletedTask;
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

    public async Task<TUnMapped?> SqlQueryAsync<TUnMapped>(
        FormattableString sql, 
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Database
            .SqlQuery<TUnMapped>(sql)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<TUnMapped>> SqlQueryListAsync<TUnMapped>(
        FormattableString sql, 
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Database.SqlQuery<TUnMapped>(sql).ToListAsync(cancellationToken);
    }
}
