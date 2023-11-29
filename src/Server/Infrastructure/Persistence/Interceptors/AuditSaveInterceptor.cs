using Application.Abstractions.Common;
using Domain.Primitives.Common;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors;

internal sealed class AuditSaveInterceptor(ITimeProvider timeProvider) : SaveChangesInterceptor
{
    private readonly ITimeProvider _timeProvider = timeProvider;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        ApplyAudit(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        await Task.Run(() => ApplyAudit(eventData.Context), cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void ApplyAudit(DbContext? dbContext)
    {
        if (dbContext is null) return;

        var userId = "Initiator";

        foreach(var entry in dbContext.ChangeTracker.Entries<IAuditableEntity>().ToList())
        {
            switch(entry.State)
            {
                case EntityState.Added: 
                    entry.Entity.Created = _timeProvider.UtcNow;
                    entry.Entity.LastModified = _timeProvider.UtcNow;
                    entry.Entity.Author = userId;
                    entry.Entity.Editor = userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModified = _timeProvider.UtcNow;
                    entry.Entity.Editor = userId;
                    break;

                case EntityState.Unchanged when HasChangeOwnedEntities(entry):
                    entry.Entity.LastModified = _timeProvider.UtcNow;
                    entry.Entity.Editor = userId;
                    break;
            }
        }
    }

    private static bool HasChangeOwnedEntities(EntityEntry entityEntry)
    {
        return entityEntry.References.Any(r => 
            r.TargetEntry is not null 
            && (r.TargetEntry.ComplexProperties.Any(x => x.IsModified)
            || (r.TargetEntry.Metadata.IsOwned() 
            && r.TargetEntry.State is EntityState.Modified or EntityState.Added)));
    }
}
