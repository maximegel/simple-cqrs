namespace SimpleCqrs.Shared.Domain.Events;

public interface IEventSourced<out TAggregate, in TEvent>
    where TAggregate : IAggregateRoot, IEventSourced<TAggregate, TEvent>
    where TEvent : IDomainEvent
{
    TAggregate Apply(TEvent domainEvent);
}