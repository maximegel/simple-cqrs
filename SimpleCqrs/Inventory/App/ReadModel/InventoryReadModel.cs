using SimpleCqrs.Inventory.App.ReadModel.Projections;
using SimpleCqrs.Inventory.App.ReadModel.Services;

namespace SimpleCqrs.Inventory.App.ReadModel;

public class InventoryReadModel
{
    public InventoryReadModel(
        ICatalogItemsQueryService catalogItems,
        IInventoryItemsProjection inventoryItems)
    {
        CatalogItems = catalogItems;
        InventoryItems = inventoryItems;
    }
    
    public ICatalogItemsQueryService CatalogItems { get; }

    public IInventoryItemsProjection InventoryItems { get; }
}