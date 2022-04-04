using SimpleCqrs.Inventory.Domain.Internal;

namespace SimpleCqrs.Inventory.Domain.Events;

public record ItemReceived(
    string CatalogId) :
    InventoryItemEvent
{
    internal override InventoryItemState ApplyTo(InventoryItemState state)
    {
        return state.Receive();
    }
}
