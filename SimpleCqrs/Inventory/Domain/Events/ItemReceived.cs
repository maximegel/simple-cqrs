using SimpleCqrs.Inventory.Domain.Internal;

namespace SimpleCqrs.Inventory.Domain.Events;

public record ItemReceived(
    string Model,
    string Category) :
    InventoryItemEvent
{
    internal override InventoryItemState ApplyTo(InventoryItemState state)
    {
        return state.Receive();
    }
}
