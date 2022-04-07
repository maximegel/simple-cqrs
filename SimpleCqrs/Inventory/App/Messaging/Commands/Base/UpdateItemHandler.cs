using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.App.Messaging.Commands.Base;

public abstract class UpdateItemHandler<TCommand> : 
    CommandHandler<TCommand> 
    where TCommand : InventoryItemCommand 
{
    private readonly IRepository<IInventoryItem> _repository;

    protected UpdateItemHandler(IRepository<IInventoryItem> repository) => 
        _repository = repository;

    protected override async Task Handle(
        TCommand command, 
        CancellationToken cancellationToken)
    {
        var inventoryItemId = InventoryItemId.Parse(command.AggregateId);
        var inventoryItem = await _repository.Find(inventoryItemId, cancellationToken);
        
        inventoryItem.Execute(command);
        await _repository.Save(inventoryItem, cancellationToken);
    }
}