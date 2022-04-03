using System.Collections;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Domain.Events;
using SimpleCqrs.Shared.Domain.Snapshots;

namespace SimpleCqrs.Shared.Infra.Persistence;

public abstract class EventDrivenPersistenceMapper<
    TAggregate, TEvent, TSnapshot, TPersistent> :
    IPersistenceMapper<TAggregate, TPersistent>
    where TAggregate : 
    IEventDriven<TAggregate, TEvent>, 
    ISnapshotable<TAggregate, TSnapshot>, 
    IAggregateRoot 
    where TEvent : IDomainEvent
    where TSnapshot : ISnapshot
    where TPersistent : IPersistent, new()
{
    public TAggregate Map(TPersistent source) =>
        Load(source);

    public void Map(TAggregate source, TPersistent destination)
    {
        destination.Identifier = source.Id;
        ApplyEvents(destination, source.UncommittedEvents);
        ApplySnapshot(destination, source.TakeSnapshot());
    }

    protected abstract TAggregate Load(TPersistent data);

    protected abstract void ApplyEvent(
        TPersistent data, 
        TEvent domainEvent);
    
    protected abstract void ApplySnapshot(
        TPersistent data, 
        TSnapshot snapshot);

    private void ApplyEvents(
        TPersistent data,
        IEnumerable domainEvents)
    {
        domainEvents
            .OfType<TEvent>()
            .ToList()
            .ForEach(e => ApplyEvent(data, e));
    }
}