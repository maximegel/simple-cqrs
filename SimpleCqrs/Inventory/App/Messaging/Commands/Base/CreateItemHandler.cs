using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.App.Messaging.Commands.Base;

public abstract class CreateItemHandler<TCommand> : 
    CommandHandler<TCommand> 
    where TCommand : InventoryItemCommand 
{
    private readonly IRepository<IInventoryItem> _repository;

    protected CreateItemHandler(IRepository<IInventoryItem> repository) => 
        _repository = repository;

    protected override async Task Handle(
        TCommand command, 
        CancellationToken cancellationToken)
    {
        var inventoryItem = Create(command);
        await _repository.Save(inventoryItem, cancellationToken);
    }

    protected abstract IInventoryItem Create(TCommand command);
}