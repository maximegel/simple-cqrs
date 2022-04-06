using MediatR;

namespace SimpleCqrs.Shared.App.Messaging.Events;

public interface IEventHandler<in TEvent> :
    INotificationHandler<TEvent>
    where TEvent : IIntegrationEvent
{
    new Task Handle(TEvent e, CancellationToken cancellationToken);
}