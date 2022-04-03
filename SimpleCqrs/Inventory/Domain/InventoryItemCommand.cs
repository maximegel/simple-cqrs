using SimpleCqrs.Inventory.Domain.Internal;
using SimpleCqrs.Shared.Domain.Commands;

namespace SimpleCqrs.Inventory.Domain;

public abstract record InventoryItemCommand : Command
{
    private protected InventoryItemCommand(string? aggregateId) :
        base(aggregateId ?? InventoryItemId.Generate())
    {
    }
    
    internal abstract IEnumerable<InventoryItemEvent> ExecuteOn(
        InventoryItemState state);
}