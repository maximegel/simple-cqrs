using SimpleCqrs.Inventory.Domain.Events;
using SimpleCqrs.Inventory.Domain.Internal;

namespace SimpleCqrs.Inventory.Domain.Commands;

public record ReceiveItem(
    string? AggregateId, 
    string CatalogId,
    string OrderId) : 
    InventoryItemCommand(AggregateId)
{
    internal override IEnumerable<InventoryItemEvent> ExecuteOn(
        InventoryItemState state)
    { 
        yield return new ItemReceived(CatalogId, OrderId);
    }
}