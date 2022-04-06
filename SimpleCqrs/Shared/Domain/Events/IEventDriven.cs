using System.Collections;

namespace SimpleCqrs.Shared.Domain.Events;

public interface IEventDriven<out TAggregate>
    where TAggregate : IAggregateRoot
{
    IEnumerable<IDomainEvent> UncommittedEvents { get; }

    TAggregate MarkAsCommitted();
}