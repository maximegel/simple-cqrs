using SimpleCqrs.Inventory.App.Messaging.Commands.Base;
using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.App.Messaging.Commands;

public class ReceiveItemHandler : CreateItemHandler<ReceiveItem>
{
    public ReceiveItemHandler(IRepository<IInventoryItem> repository) :
        base(repository)
    {
    }

    protected override IInventoryItem Create(ReceiveItem command) =>
        InventoryItemFactory.Receive(command);
}