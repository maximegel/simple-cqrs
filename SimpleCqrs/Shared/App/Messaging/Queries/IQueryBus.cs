namespace SimpleCqrs.Shared.App.Messaging.Queries;

public interface IQueryBus
{
    Task<TResult> Send<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default);
}