using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.App.Messaging.Commands;

public class RelocateItemHandler : CommandHandler<InventoryItemCommand>
{
    private readonly IRepository<IInventoryItem> _repository;

    public RelocateItemHandler(IRepository<IInventoryItem> repository) =>
        _repository = repository;
    
    protected override async Task Handle(
        InventoryItemCommand command, 
        CancellationToken cancellationToken)
    {
        var inventoryItemId = InventoryItemId.Parse(command.AggregateId);
        var inventoryItem = await _repository.Find(inventoryItemId, cancellationToken);

        inventoryItem.Execute(command);
        await _repository.Save(inventoryItem, cancellationToken);
    }
}