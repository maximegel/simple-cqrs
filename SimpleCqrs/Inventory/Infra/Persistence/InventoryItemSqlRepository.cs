using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Infra.Persistence.DataModels;
using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Inventory.Infra.Persistence;

internal class InventoryItemSqlRepository : 
    IRepository<IInventoryItem>
{
    private readonly InventorySqlContext _dbContext;
    private readonly DbSet<InventoryItemData> _dbSet;

    public InventoryItemSqlRepository(InventorySqlContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.InventoryItems;
    }

    public async Task<IInventoryItem> Find(
        IIdentifier id, 
        CancellationToken cancellationToken = default)
    {
        var found = 
            // Hack for SQLite in-memory.
            _dbSet.Local.FirstOrDefault(i => i.Id.ToString() == id.ToString())
            ?? await _dbSet.FirstOrDefaultAsync(
                i => i.Id.ToString() == id.ToString(), 
                cancellationToken)
            ?? throw new AggregateNotFoundException();

        return InventoryItemSqlFactory.LoadFromData(id, found);
    }

    public async Task Save(
        IInventoryItem aggregate, 
        CancellationToken cancellationToken = default)
    {
        aggregate.TakeSnapshot();
        var id = (Guid)aggregate.Id;
        var found = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        if (found == null)
        {
            var added = new InventoryItemData { Id = id };
            ApplyChanges(added, aggregate);
            _dbSet.Add(added);
        }
        else
        {
            ApplyChanges(found, aggregate);
            _dbSet.Update(found);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void ApplyChanges(
        InventoryItemData data, 
        IInventoryItem aggregate)
    {
        var events = aggregate.UncommittedEvents;
        ApplyEvents(data, events);
        
        var snapshot = aggregate.TakeSnapshot();
        ApplySnapshot(data, snapshot);
    }
    
    private static void ApplyEvents(
        InventoryItemData data, 
        IEnumerable<IDomainEvent> domainEvents)
    {
        domainEvents
            .OfType<InventoryItemEvent>()
            .ToList()
            .ForEach(data.ApplyEvent);
    }
    
    private static void ApplySnapshot(
        InventoryItemData data, 
        InventoryItemSnapshot snapshot)
    {
        data.ApplySnapshot(snapshot);
    }
}