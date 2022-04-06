using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Events;
using SimpleCqrs.Inventory.Persistence.Internal;
using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Inventory.Persistence;

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

    public async Task<IInventoryItem?> Find(
        IIdentifier id, 
        CancellationToken cancellationToken = default)
    {
        var found = 
            // Hack for SQLite in-memory.
            _dbSet.Local.FirstOrDefault(i => i.Id.ToString() == id.ToString())
            ?? await _dbSet.FirstOrDefaultAsync(
                i => i.Id.ToString() == id.ToString(), 
                cancellationToken);

        if (found == null) 
            return null;

        return InventoryItemFactory.LoadFromSnapshot(
            (InventoryItemId)id,
            new InventoryItemSnapshot(found.Status));
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
            ApplyChanges(added, aggregate.UncommittedEvents);
            _dbSet.Add(added);
        }
        else
        {
            ApplyChanges(found, aggregate.UncommittedEvents);
            _dbSet.Update(found);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void ApplyChanges(
        InventoryItemData data, 
        IDomainEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ItemReceived(var catalogId):
                data.CatalogId = Guid.Parse(catalogId);
                break;
            case ItemRelocated e:
                data.StorageLocation = e.StorageLocation;
                break;
            case InventoryItemSnapshot e:
                data.Status = e.Status;
                break;
        } 
    }
    
    private static void ApplyChanges(
        InventoryItemData data, 
        IEnumerable<IDomainEvent> domainEvents)
    {
        domainEvents
            .ToList()
            .ForEach(domainEvent => ApplyChanges(data, domainEvent));
    }
}