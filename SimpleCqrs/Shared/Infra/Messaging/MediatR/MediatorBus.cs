using MediatR;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Messaging.Queries;
using SimpleCqrs.Shared.Domain.Commands;

namespace SimpleCqrs.Shared.Infra.Messaging.MediatR;

public class MediatorBus :
    ICommandBus,
    IQueryBus
{
    private readonly IMediator _mediator;

    public MediatorBus(IMediator mediator) =>
        _mediator = mediator;

    public Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        return _mediator.Send(CommandEnvelope.Of(command), cancellationToken);
    }
    
    public Task<TResult> Send<TResult>(
        IQuery<TResult> query, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(query, cancellationToken);
    }
}