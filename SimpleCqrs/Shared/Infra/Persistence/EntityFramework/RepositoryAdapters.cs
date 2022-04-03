using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Shared.Infra.Persistence.EntityFramework;

public static class RepositoryAdapters
{
    public static IRepository<TAggregate> AsRepository<TAggregate>(
        this DbContext dbContext) 
        where TAggregate : class, IAggregateRoot
    {
        return new DbRepository<TAggregate>(dbContext);
    }
}