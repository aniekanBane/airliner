using Domain.Primitives.Common;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Persistence.Interceptors;

public class DomainEventInterceptor(
    IPublisher publisher, 
    IAppLogger<DomainEventInterceptor> logger) : SaveChangesInterceptor
{
    private readonly IAppLogger<DomainEventInterceptor> _logger = logger;
    private readonly IPublisher _publisher = publisher;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData, 
        InterceptionResult<int> result)
    {
        PublishEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        await PublishEvents(eventData.Context, cancellationToken);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishEvents(
        DbContext? dbContext, 
        CancellationToken cancellationToken = default)
    {
        if (dbContext is null) return;

        var entities = dbContext.ChangeTracker
            .Entries<IEntity>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();

        if (entities.Count == 0) return;

        var domainEvents = entities.SelectMany(e => e.DomainEvents).ToList();
        entities.ForEach(e => e.ClearDomainEvents());

        foreach (var evnt in domainEvents)
        {
            await _publisher.Publish(evnt, cancellationToken);
        }
    }
}
