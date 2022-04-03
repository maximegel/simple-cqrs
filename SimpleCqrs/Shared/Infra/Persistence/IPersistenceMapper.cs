namespace SimpleCqrs.Shared.Infra.Persistence;

public interface IPersistenceMapper<TSource, in TDestination>
{
    TSource Map(TDestination source);

    void Map(TSource source, TDestination destination);
}