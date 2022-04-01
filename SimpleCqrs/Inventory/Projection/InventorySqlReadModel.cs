using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.App.Projection;
using SimpleCqrs.Inventory.Persistence;

namespace SimpleCqrs.Inventory.Projection;

public class InventorySqlReadModel : IInventoryReadModel
{
    private readonly InventorySqlContext _dbContext;

    public InventorySqlReadModel(InventorySqlContext dbContext) => 
        _dbContext = dbContext;

    public IAsyncQueryable<InventoryItemProjection> InventoryItems =>
        _dbContext.InventoryItems
            .Select(i => new InventoryItemProjection
            {
                Id = i.Id,
                Model = i.Model,
                Category = i.Category
            })
            .AsNoTracking()
            .AsAsyncEnumerable()
            .AsAsyncQueryable();
}