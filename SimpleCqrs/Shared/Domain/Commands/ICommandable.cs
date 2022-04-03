namespace SimpleCqrs.Shared.Domain.Commands;

public interface ICommandable<out TAggregate, in TCommand>
    where TAggregate : IAggregateRoot, ICommandable<TAggregate, TCommand>
    where TCommand : ICommand
{
    TAggregate Execute(TCommand command);
}