using SimpleCqrs.Shared.App.Messaging.Events;

namespace SimpleCqrs.Shared.App.Persistence.Events;

public interface IEventPublisher
{
    Task Publish<TEvent>(
        IEnumerable<TEvent> events,
        CancellationToken cancellationToken = default)
        where TEvent : IIntegrationEvent;
}