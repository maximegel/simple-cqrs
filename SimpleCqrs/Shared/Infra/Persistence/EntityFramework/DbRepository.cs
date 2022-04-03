using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Shared.Infra.Persistence.EntityFramework;

public class DbRepository<TAggregate> :
    IRepository<TAggregate> 
    where TAggregate : class, IAggregateRoot
{
    private readonly DbContext _dbContext;
    private readonly DbSet<TAggregate> _dbSet;

    public DbRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TAggregate>();
    }
    
    public async Task<TAggregate?> Find(
        IIdentifier id, 
        CancellationToken cancellationToken = default)
    {
        return FindLocal(id) 
               ?? await _dbSet
                   .FirstOrDefaultAsync(ById(id), cancellationToken);
    }

    public async Task Save(
        TAggregate aggregate, 
        CancellationToken cancellationToken = default)
    {
        var found = await Find(aggregate.Id, cancellationToken);
        if (found == null) _dbSet.Add(aggregate);
        else _dbSet.Update(aggregate);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    private TAggregate? FindLocal(IIdentifier id)
    {
        var byId = ById(id).Compile();
        return _dbSet.Local.FirstOrDefault(byId);
    }

    private static Expression<Func<TAggregate, bool>> ById(IIdentifier id) =>
        agg => agg.Id.ToString() == id.ToString();
}