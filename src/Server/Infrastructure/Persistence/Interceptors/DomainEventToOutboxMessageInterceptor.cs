using Application.Abstractions.Common;
using Domain.Primitives.Common;
using Persistence.Models.Messaging;

using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json;

namespace Persistence.Interceptors;

internal sealed class DomainEventToOutboxMessageInterceptor(ITimeProvider timeProvider) 
    : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PersistOutboxMessages(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        PersistOutboxMessages(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void PersistOutboxMessages(DbContext? dbContext)
    {
        if (dbContext is null) return;

        var entities = dbContext.ChangeTracker
            .Entries<IEntity>()
            .Where(x => x.Entity.DomainEvents.Count != 0)
            .Select(x => x.Entity)
            .ToList();

        if (entities.Count == 0)
            return;

        var domainEvents = entities.SelectMany(x => x.DomainEvents).ToList();
        entities.ForEach(x => x.ClearDomainEvents());
        
        var messages = domainEvents.Select(x => new OutboxMessage(
            x.GetType().Name,
            JsonSerializer.Serialize(x),
            timeProvider.UtcNow
        )).ToList();

        dbContext.Set<OutboxMessage>().AddRange(messages);
    }
}
