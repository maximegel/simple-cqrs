using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Shared.App.Persistence;

public interface IRepository<TAggregate>
    where TAggregate : IAggregateRoot
{
    Task<TAggregate?> Find(
        IIdentifier id, 
        CancellationToken cancellationToken = default);

    Task Save(
        TAggregate aggregate, 
        CancellationToken cancellationToken = default);
}