using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Inventory.Domain;

public interface IInventoryItem : IAggregateRoot<InventoryItemId>
{
    public string Model { get; }

    public string Category { get; }
}