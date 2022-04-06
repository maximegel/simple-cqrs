using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.App.Messaging.Commands;

public class RelocateItemHandler : CommandHandler<RelocateItem>
{
    private readonly IRepository<IInventoryItem> _repository;

    public RelocateItemHandler(IRepository<IInventoryItem> repository) =>
        _repository = repository;
    
    protected override async Task Handle(
        RelocateItem command, 
        CancellationToken cancellationToken)
    {
        var inventoryItemId = InventoryItemId.Parse(command.AggregateId);
        var inventoryItem =
            await _repository.Find(inventoryItemId, cancellationToken)
            ?? throw new Exception("Aggregate not found.");

        inventoryItem.Execute(command);
        await _repository.Save(inventoryItem, cancellationToken);
    }
}