using MediatR;
using SimpleCqrs.Shared.App.Messaging.Events;
using SimpleCqrs.Shared.App.Persistence.Events;

namespace SimpleCqrs.Shared.Infra.Messaging.MediatR;

public class MediatorEventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;

    public MediatorEventPublisher(IMediator mediator) => 
        _mediator = mediator;

    public Task Publish<TEvent>(
        IEnumerable<TEvent> events, 
        CancellationToken cancellationToken = default) 
        where TEvent : IIntegrationEvent
    {
        var tasks = events
            .Select(e => _mediator.Publish(e, cancellationToken))
            .ToArray();

        return Task.WhenAll(tasks);
    }
}