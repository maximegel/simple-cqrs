using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Persistence.Internal;
using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Inventory.Persistence;

public class InventoryItemSqlRepository : IRepository<IInventoryItem>
{
    private readonly InventorySqlContext _dbContext;

    public InventoryItemSqlRepository(InventorySqlContext dbContext) => 
        _dbContext = dbContext;

    public async Task<IInventoryItem?> Find(IIdentifier id, CancellationToken cancellationToken = default)
    {
        var model = await _dbContext.InventoryItems
            .FirstOrDefaultAsync(i => i.Id.ToString() == id.ToString(), cancellationToken);
        return model == null 
            ? null 
            : InventoryItemFactory.Load(
                InventoryItemId.Parse(model.Id), 
                model.Model, 
                model.Category);
    }

    public async Task Save(IInventoryItem aggregate, CancellationToken cancellationToken = default)
    {
        var id = (Guid)aggregate.Id;
        var data = await _dbContext.InventoryItems.FindAsync(new object[] { id }, cancellationToken);
        if (data == null)
        {
            var newData = new InventoryItemData { Id = id };
            Map(aggregate, newData);
            _dbContext.InventoryItems.Add(newData);
        }
        else
        {
            Map(aggregate, data);
            _dbContext.InventoryItems.Update(data);
        }
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    private static void Map(IInventoryItem source, InventoryItemData destination)
    {
        destination.Model = source.Model;
        destination.Category = source.Category;
    }
}