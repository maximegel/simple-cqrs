namespace SimpleCqrs.Shared.Domain.Snapshots;

public interface ISnapshotable<out TAggregate>
    where TAggregate : IAggregateRoot
{
    TAggregate TakeSnapshot();
}