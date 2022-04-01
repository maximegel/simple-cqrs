using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.App.Messaging.Commands;

public class ReceiveItemHandler : CommandHandler<ReceiveItem>
{
    private readonly IRepository<IInventoryItem> _repository;

    public ReceiveItemHandler(IRepository<IInventoryItem> repository) =>
        _repository = repository;
    
    protected override async Task Handle(ReceiveItem command, CancellationToken cancellationToken)
    {
        var inventoryItem = InventoryItemFactory.Receive(command);
        await _repository.Save(inventoryItem, cancellationToken);
    }
}