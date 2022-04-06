namespace SimpleCqrs.Shared.Domain.Events;

public interface IEventSourced<out TAggregate>
    where TAggregate : IAggregateRoot, IEventSourced<TAggregate>
{
    TAggregate Apply(IDomainEvent domainEvent);
}