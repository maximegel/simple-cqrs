using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Infra.Persistence.Internal;

namespace SimpleCqrs.Shared.Infra.Persistence;

public static class RepositoryDecorators
{
    public static IRepository<TAggregate> UseMapper<TAggregate, TData>(
        this IRepository<TData> repository,
        IPersistenceMapper<TAggregate, TData> mapper)
        where TAggregate : IAggregateRoot
        where TData : Persistent<TData>, new()
    {
        return new MappingRepository<TAggregate, TData>(
            repository,
            mapper);
    }
}