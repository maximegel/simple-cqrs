using System.Collections;

namespace SimpleCqrs.Shared.Domain.Events;

public abstract class EventDrivenAggregateRoot<TSelf, TId, TEvent> : 
    AggregateRoot<TSelf, TId>,
    IEventDriven<TSelf, TEvent>
    where TSelf : class, IAggregateRoot<TId>, IEventDriven<TSelf, TEvent>
    where TId : IIdentifier
    where TEvent : IDomainEvent
{
    private IEnumerable<TEvent> _uncommittedEvents = Enumerable.Empty<TEvent>();

    protected EventDrivenAggregateRoot(TId id) : base(id)
    {
    }

    IEnumerable<TEvent> IEventDriven<TSelf, TEvent>.UncommittedEvents =>
        _uncommittedEvents;
    
    IEnumerable IEventDriven<TSelf>.UncommittedEvents => 
        _uncommittedEvents;

    TSelf IEventDriven<TSelf>.MarkAsCommitted()
    {
        _uncommittedEvents = Enumerable.Empty<TEvent>();
        return AsSelf();
    }
    
    protected void Raise(params TEvent[] domainEvents)
    {
        OnRaising(domainEvents);
        _uncommittedEvents = _uncommittedEvents.Concat(domainEvents);
    }

    protected void Raise(IEnumerable<TEvent> domainEvents) =>
        Raise(domainEvents.ToArray());

    private protected abstract void OnRaising(IEnumerable<TEvent> domainEvents);
    
    private protected TSelf AsSelf() =>
        this as TSelf
        ?? throw new InvalidCastException(
            $"{GetType().Name} must be assignable to {typeof(TSelf).Name}.");
}