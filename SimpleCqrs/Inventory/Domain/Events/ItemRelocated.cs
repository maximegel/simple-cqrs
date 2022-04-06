using SimpleCqrs.Inventory.Domain.Internal;

namespace SimpleCqrs.Inventory.Domain.Events;

public record ItemRelocated(
    string StorageLocation) :
    InventoryItemEvent
{
    internal override InventoryItemState ApplyTo(InventoryItemState state)
    {
        return state;
    }
}