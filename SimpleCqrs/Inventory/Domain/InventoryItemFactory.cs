using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Inventory.Domain.Internal;

namespace SimpleCqrs.Inventory.Domain;

public static class InventoryItemFactory
{
    public static IInventoryItem Receive(ReceiveItem command)
    {
        var id = InventoryItemId.Parse(command.AggregateId);
        return new InventoryItem(id).Receive(command);
    }
    
    public static IInventoryItem Load(InventoryItemId id, string model, string category) =>
        new InventoryItem(id)
        {
            Model = model,
            Category = category
        };
}