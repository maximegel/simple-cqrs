using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Infra.Persistence.DataModels;
using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Inventory.Infra.Persistence;

public static class InventoryItemSqlFactory
{
    public static IInventoryItem LoadFromData(
        IIdentifier id,
        InventoryItemData data)
    {
        var snapshot = new InventoryItemSnapshot(
            data.Status);

        return InventoryItemFactory.LoadFromSnapshot(
            (InventoryItemId)id,
            snapshot);
    }
}