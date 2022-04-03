using System.Collections;

namespace SimpleCqrs.Shared.Domain.Events;

public interface IEventDriven<out TAggregate>
    where TAggregate : IAggregateRoot
{
    IEnumerable UncommittedEvents { get; }

    TAggregate MarkAsCommitted();
}

public interface IEventDriven<out TAggregate, out TEvent> :
    IEventDriven<TAggregate>
    where TAggregate : IAggregateRoot
    where TEvent : IDomainEvent
{
    new IEnumerable<TEvent> UncommittedEvents { get; }
}