namespace SimpleCqrs.Shared.Domain.Snapshots;

public interface ISnapshotable<out TAggregate, TSnapshot>
    where TAggregate : IAggregateRoot
    where TSnapshot : ISnapshot
{
    TSnapshot TakeSnapshot();

    TAggregate RestoreSnapshot(TSnapshot snapshot);
}