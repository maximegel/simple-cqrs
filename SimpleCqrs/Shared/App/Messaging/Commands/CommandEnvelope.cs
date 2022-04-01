using MediatR;
using SimpleCqrs.Shared.Domain.Commands;

namespace SimpleCqrs.Shared.App.Messaging.Commands;

public static class CommandEnvelope
{
    public static CommandEnvelope<TCommand> Of<TCommand>(TCommand command)
        where TCommand : ICommand =>
        new(command);
}

public record CommandEnvelope<TCommand>(TCommand Payload) : IRequest
    where TCommand : ICommand
{
    public TCommand AsCommand() => Payload;
}