using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Domain.Commands;
using SimpleCqrs.Shared.Domain.Events;
using SimpleCqrs.Shared.Domain.Snapshots;

namespace SimpleCqrs.Inventory.Domain;

public interface IInventoryItem : 
    IAggregateRoot<InventoryItemId>,
    ICommandable<IInventoryItem, InventoryItemCommand>,
    IEventDriven<IInventoryItem>,
    IEventSourced<IInventoryItem>,
    ISnapshotable<IInventoryItem, InventoryItemSnapshot> 
{
}