using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Events;
using SimpleCqrs.Inventory.Infra.Persistence.DataModels;

namespace SimpleCqrs.Inventory.Infra.Persistence;

public static class InventoryItemSqlProjector
{
    public static void ApplyEvent(
        this InventoryItemData data,
        InventoryItemEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ItemReceived e:
                data.CatalogId = Guid.Parse(e.CatalogId);
                break;
            case ItemRelocated e:
                data.StorageLocation = e.StorageLocation;
                break;
        }
    }
    
    public static void ApplySnapshot(
        this InventoryItemData data,
        InventoryItemSnapshot s)
    {
        data.Status = s.Status;
    }
}