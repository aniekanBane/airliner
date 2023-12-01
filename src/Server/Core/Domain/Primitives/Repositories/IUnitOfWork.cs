namespace Domain.Primitives.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task RollBack(CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
