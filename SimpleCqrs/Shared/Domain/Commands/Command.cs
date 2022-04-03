namespace SimpleCqrs.Shared.Domain.Commands;

public abstract record Command(
    string AggregateId) : 
    ICommand;