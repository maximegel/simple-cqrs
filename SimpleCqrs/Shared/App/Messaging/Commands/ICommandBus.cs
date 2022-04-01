using SimpleCqrs.Shared.Domain.Commands;

namespace SimpleCqrs.Shared.App.Messaging.Commands;

public interface ICommandBus
{
    Task Send<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand; 
}