using MediatR;

namespace SimpleCqrs.Shared.App.Messaging.Queries;

public abstract class QueryHandler<TQuery, TResult> :
    IRequestHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<TResult> IRequestHandler<TQuery, TResult>.Handle(
        TQuery request,
        CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }

    protected abstract Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}