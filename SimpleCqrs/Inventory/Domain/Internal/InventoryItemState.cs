using SimpleCqrs.Shared.Domain.ValueObjects;

namespace SimpleCqrs.Inventory.Domain.Internal;

internal abstract record InventoryItemState
{
    public abstract InventoryItemStatus Status { get; }
    
    public virtual InventoryItemState Receive() => this;
}