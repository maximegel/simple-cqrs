using MediatR;
using SimpleCqrs.Shared.Domain.Commands;

namespace SimpleCqrs.Shared.App.Messaging.Commands;

public abstract class CommandHandler<TCommand> :
    AsyncRequestHandler<CommandEnvelope<TCommand>>
    where TCommand : ICommand
{
    protected override Task Handle(
        CommandEnvelope<TCommand> envelope,
        CancellationToken cancellationToken)
    {
        return Handle(envelope.Payload, cancellationToken);
    }

    protected abstract Task Handle(TCommand command, CancellationToken cancellationToken);
}