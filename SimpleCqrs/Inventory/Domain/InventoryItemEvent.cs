using SimpleCqrs.Inventory.Domain.Internal;
using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Inventory.Domain;

public abstract record InventoryItemEvent : IDomainEvent
{
    internal virtual InventoryItemState ApplyTo(
        InventoryItemState state)
    {
        return state;
    }
}