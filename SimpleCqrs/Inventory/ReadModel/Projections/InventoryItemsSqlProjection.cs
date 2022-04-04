using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.App.ReadModel.Projections;
using SimpleCqrs.Inventory.Persistence;
using SimpleCqrs.Shared.Infra.ReadModel.Projections;

namespace SimpleCqrs.Inventory.ReadModel.Projections;

internal class InventoryItemsSqlProjection : 
    QueryableProjection<InventoryItemModel>,
    IInventoryItemsProjection
{
    private readonly InventorySqlContext _dbContext;

    public InventoryItemsSqlProjection(InventorySqlContext dbContext) => 
        _dbContext = dbContext;

    protected override IAsyncEnumerable<InventoryItemModel> Query =>
        _dbContext.InventoryItems
            .AsNoTracking()
            .Select(m => new InventoryItemModel
            {
                Id = m.Id.ToString(),
                CatalogId = m.CatalogId.ToString()
            })
            .AsAsyncEnumerable();
}