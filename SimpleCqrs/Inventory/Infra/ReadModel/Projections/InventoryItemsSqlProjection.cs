using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.App.ReadModel.Projections;
using SimpleCqrs.Inventory.Infra.Persistence;
using SimpleCqrs.Shared.Infra.ReadModel.Projections;

namespace SimpleCqrs.Inventory.Infra.ReadModel.Projections;

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
            .Select(data => new InventoryItemModel
            {
                Id = data.Id.ToString(),
                CatalogId = data.CatalogId.ToString(),
                Status = data.Status,
                StorageLocation = data.StorageLocation
            })
            .AsAsyncEnumerable();
}