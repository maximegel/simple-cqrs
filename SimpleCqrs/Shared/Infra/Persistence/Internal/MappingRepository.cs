using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Shared.Infra.Persistence.Internal;

internal class MappingRepository<TAggregate, TData> :
    IRepository<TAggregate>
    where TAggregate : IAggregateRoot
    where TData : Persistent<TData>, new()
{
    private readonly IRepository<TData> _decorated;
    private readonly IPersistenceMapper<TAggregate, TData> _mapper;

    public MappingRepository(
        IRepository<TData> decorated,
        IPersistenceMapper<TAggregate, TData> mapper)
    {
        _decorated = decorated;
        _mapper = mapper;
    }

    public async Task<TAggregate?> Find(
        IIdentifier id, 
        CancellationToken cancellationToken = default)
    {
        var data = await _decorated.Find(id, cancellationToken);
        return data == null ? default : _mapper.Map(data);
    }

    public async Task Save(
        TAggregate aggregate, 
        CancellationToken cancellationToken = default)
    {
        var data = 
            await _decorated.Find(aggregate.Id, cancellationToken)
            ?? new TData();
        _mapper.Map(aggregate, data);
        await _decorated.Save(data, cancellationToken);
    }
}