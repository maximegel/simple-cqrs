using MediatR;
using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Shared.App.Messaging.Events;

public interface IIntegrationEvent : 
    INotification
{
    string AggregateId { get; }
}

public interface IIntegrationEvent<out TPayload> :
    IIntegrationEvent
    where TPayload : IDomainEvent
{
    TPayload Payload { get; }
}