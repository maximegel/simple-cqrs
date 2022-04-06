namespace SimpleCqrs.Shared.Domain.Events;

public abstract class EventSourcedAggregateRoot<TSelf, TId, TEvent> :
    EventDrivenAggregateRoot<TSelf, TId, TEvent>,
    IEventSourced<TSelf>
    where TSelf : class, IAggregateRoot<TId>, IEventDriven<TSelf>, IEventSourced<TSelf>
    where TId : IIdentifier
    where TEvent : IDomainEvent
{
    protected EventSourcedAggregateRoot(TId id) : base(id)
    {
    }

    TSelf IEventSourced<TSelf>.Apply(IDomainEvent domainEvent) =>
        domainEvent is TEvent e ? Apply(e) : AsSelf();

    public abstract TSelf Apply(TEvent domainEvent);
    
    protected TSelf Apply(IEnumerable<TEvent> domainEvents) =>
        domainEvents.Aggregate(AsSelf(), (agg, e) => agg.Apply(e));

    private protected override void OnRaising(
        IEnumerable<TEvent> domainEvents)
    {
        Apply(domainEvents);
    }

    
}