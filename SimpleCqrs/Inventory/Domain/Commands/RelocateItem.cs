using SimpleCqrs.Inventory.Domain.Events;
using SimpleCqrs.Inventory.Domain.Internal;

namespace SimpleCqrs.Inventory.Domain.Commands;

public record RelocateItem(
    string? AggregateId, 
    string StorageLocation) : 
    InventoryItemCommand(AggregateId)
{
    internal override IEnumerable<InventoryItemEvent> ExecuteOn(
        InventoryItemState state)
    { 
        yield return new ItemRelocated(StorageLocation);
    }
}