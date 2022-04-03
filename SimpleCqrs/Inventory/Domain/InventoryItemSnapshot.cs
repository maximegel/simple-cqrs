using SimpleCqrs.Inventory.Domain.Internal;
using SimpleCqrs.Inventory.Domain.Internal.States;
using SimpleCqrs.Shared.Domain.Snapshots;

namespace SimpleCqrs.Inventory.Domain;

public record InventoryItemSnapshot(
    InventoryItemStatus Status) : 
    ISnapshot
{
    internal static InventoryItemSnapshot Capture(
        InventoryItemState state)
    {
        return new InventoryItemSnapshot(
            state.Status);
    }

    internal InventoryItemState Restore()
    {
        return Status switch
        {
            InventoryItemStatus.InStock => new InStock(),
            InventoryItemStatus.OutOfStock => new OutOfStock(),
            _ => new OutOfStock()
        };
    }
}