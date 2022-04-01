using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.Domain;

namespace SimpleCqrs.Inventory.Domain.Internal;

internal class InventoryItem : 
    AggregateRoot<IInventoryItem, InventoryItemId>,
    IInventoryItem
{
    public InventoryItem(InventoryItemId id) : base(id)
    {
    }

    public string Model { get; internal set; } = null!;

    public string Category { get; internal set; } = null!;

    public IInventoryItem Receive(ReceiveItem command)
    {
        var (_, model, category) = command;
        Model = model;
        Category = category;
        return this;
    }
}