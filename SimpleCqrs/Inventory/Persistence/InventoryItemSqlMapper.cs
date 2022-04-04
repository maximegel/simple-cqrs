using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Events;
using SimpleCqrs.Inventory.Persistence.Internal;
using SimpleCqrs.Shared.Infra.Persistence;

namespace SimpleCqrs.Inventory.Persistence;

internal class InventoryItemSqlMapper :
    EventDrivenPersistenceMapper<
        IInventoryItem, 
        InventoryItemEvent, 
        InventoryItemSnapshot, 
        InventoryItemData>
{
    protected override IInventoryItem Load(
        InventoryItemData data)
    {
        return InventoryItemFactory.LoadFromSnapshot(
            InventoryItemId.Parse(data.Id),
            new InventoryItemSnapshot(data.Status));
    }
    
    protected override void ApplyEvent(
        InventoryItemData data,
        InventoryItemEvent domainEvent)
    {
        switch (domainEvent)
        {
            case ItemReceived(var catalogId):
                data.CatalogId = Guid.Parse(catalogId);
                break;
        }
    }
    
    protected override void ApplySnapshot(
        InventoryItemData data,
        InventoryItemSnapshot snapshot)
    {
        data.Status = snapshot.Status;
    }
}