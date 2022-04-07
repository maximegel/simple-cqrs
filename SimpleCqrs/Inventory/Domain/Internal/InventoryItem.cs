using SimpleCqrs.Inventory.Domain.Internal.States;
using SimpleCqrs.Shared.Domain.Events;

namespace SimpleCqrs.Inventory.Domain.Internal;

internal class InventoryItem : 
    EventSourcedAggregateRoot<IInventoryItem, InventoryItemId, InventoryItemEvent>,
    IInventoryItem
{
    public InventoryItem(InventoryItemId id) : base(id)
    {
    }

    private InventoryItemState State { get; set; } = new OutOfStock();

    public IInventoryItem Execute(InventoryItemCommand command)
    { 
        var domainEvents = command.ExecuteOn(State);
        Raise(domainEvents);
        return this;
    }
    
    public override IInventoryItem Apply(InventoryItemEvent domainEvent)
    {
        State = domainEvent.ApplyTo(State);
        return this;
    }

    public InventoryItemSnapshot TakeSnapshot() => 
        InventoryItemSnapshot.Capture(State);

    public IInventoryItem RestoreSnapshot(InventoryItemSnapshot snapshot) =>
        new InventoryItem(Id) { State = snapshot.Restore() };
}