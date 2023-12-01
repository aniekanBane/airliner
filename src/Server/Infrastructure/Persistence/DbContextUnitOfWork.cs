using Domain.Primitives.Repositories;

namespace Persistence;

internal abstract class DbContextUnitOfWork<TDbContext> : DbContext, IUnitOfWork
    where TDbContext : DbContext
{
    protected DbContextUnitOfWork(DbContextOptions<TDbContext> options) : base(options)
    {
    }

    public Task RollBack(CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }
}
