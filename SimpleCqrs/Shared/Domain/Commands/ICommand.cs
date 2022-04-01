namespace SimpleCqrs.Shared.Domain.Commands;

public interface ICommand
{
    string AggregateId { get; }
}