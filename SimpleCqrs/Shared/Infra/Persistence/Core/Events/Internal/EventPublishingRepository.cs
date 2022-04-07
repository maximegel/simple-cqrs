using SimpleCqrs.Shared.App.Messaging.Events;
using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.App.Persistence.Events;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Shared.Infra.Persistence.Core.Events.Internal;

internal class EventPublishingRepository<TAggregate> :
    IRepository<TAggregate> 
    where TAggregate : IEventDriven<TAggregate>, IAggregateRoot
{
    private readonly IRepository<TAggregate> _inner;
    private readonly IEventPublisher _publisher;

    public EventPublishingRepository(
        IRepository<TAggregate> inner,
        IEventPublisher publisher)
    {
        _inner = inner;
        _publisher = publisher;
    }

    public Task<TAggregate> Find(
        IIdentifier id, 
        CancellationToken cancellationToken = default)
    {
        return _inner.Find(id, cancellationToken);
    }

    public async Task Save(
        TAggregate aggregate, 
        CancellationToken cancellationToken = default)
    {
        await _inner.Save(aggregate, cancellationToken);
        
        var integrationEvents = aggregate.UncommittedEvents
            .Select(e => IntegrationEvent.Create(aggregate.Id, e));
        await _publisher.Publish(integrationEvents, cancellationToken);
        aggregate.MarkAsCommitted();
    }
}