using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.App.Persistence.Events;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Domain.Events;
using SimpleCqrs.Shared.Infra.Persistence.Core.Events.Internal;

namespace SimpleCqrs.Shared.Infra.Persistence.Core.Events;

public static class RepositoryDecorators
{
    public static IRepository<TAggregate> UseImmediatePublisher<TAggregate>(
        this IRepository<TAggregate> repository,
        IEventPublisher publisher)
        where TAggregate : IAggregateRoot, IEventDriven<TAggregate>
    {
        return new EventPublishingRepository<TAggregate>(
            repository,
            publisher);
    }
}
