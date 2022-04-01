using MediatR;

namespace SimpleCqrs.Shared.App.Messaging.Queries;

public interface IQuery<out TResult> : IRequest<TResult>
{
}