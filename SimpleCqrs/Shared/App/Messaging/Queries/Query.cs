namespace SimpleCqrs.Shared.App.Messaging.Queries;

public abstract record Query<TResult> : IQuery<TResult>;