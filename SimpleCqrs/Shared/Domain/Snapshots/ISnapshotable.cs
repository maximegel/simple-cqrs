namespace SimpleCqrs.Shared.Domain.Snapshots;

public interface ISnapshotable<out TEntity, TSnapshot>
    where TEntity : IEntity
    where TSnapshot : ISnapshot
{
    TSnapshot TakeSnapshot();
    
    TEntity RestoreSnapshot(TSnapshot snapshot);
}