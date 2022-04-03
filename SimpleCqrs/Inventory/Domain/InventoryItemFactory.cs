using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Inventory.Domain.Internal;

namespace SimpleCqrs.Inventory.Domain;

public static class InventoryItemFactory
{
    public static IInventoryItem Receive(ReceiveItem command)
    {
        var id = InventoryItemId.Parse(command.AggregateId);
        return new InventoryItem(id).Execute(command);
    }
    
    public static IInventoryItem LoadFromSnapshot(
        InventoryItemId id, InventoryItemSnapshot snapshot) =>
        Load(id).RestoreSnapshot(snapshot);

    private static IInventoryItem Load(InventoryItemId id) =>
        new InventoryItem(id);
}