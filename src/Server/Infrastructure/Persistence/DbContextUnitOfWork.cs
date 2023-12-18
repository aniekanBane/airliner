using Domain.Entities.Shared.Storage;
using Domain.Infrastructure.Storage;
using Domain.Primitives.Repositories;
using Persistence.Configurations;
using Persistence.Interceptors;

namespace Persistence;

internal abstract class DbContextUnitOfWork<TDbContext>(
    DbContextOptions<TDbContext> options,
    AuditSaveInterceptor auditSaveInterceptor,
    DomainEventToOutboxMessageInterceptor outboxMessageInterceptor
) 
    : DbContext(options), IUnitOfWork where TDbContext : DbContext
{
    public DbSet<FileEntry> FileEntries => Set<FileEntry>();

    public Task RollBack(CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditSaveInterceptor, outboxMessageInterceptor);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new FileEntryConfiguration());
        modelBuilder.HasPostgresEnum<FileType>();
    }
}
