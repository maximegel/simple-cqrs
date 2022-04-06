using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Shared.App.Messaging.Events;

public static class IntegrationEvent
{
    public static IIntegrationEvent<TEvent> Create<TEvent>(
        IIdentifier aggregateId,
        TEvent domainEvent) 
        where TEvent : IDomainEvent
    {
        var type = typeof(IntegrationEvent<>).MakeGenericType(domainEvent.GetType());
        var id = aggregateId.ToString();
        var instance = Activator.CreateInstance(type, id, domainEvent)!;
        return (IIntegrationEvent<TEvent>)instance;
    }
}
    
public record IntegrationEvent<TEvent>(
    string AggregateId,
    TEvent Payload) : 
    IIntegrationEvent<TEvent> 
    where TEvent : IDomainEvent;