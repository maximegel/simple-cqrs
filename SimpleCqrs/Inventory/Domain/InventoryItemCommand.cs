using SimpleCqrs.Shared.Domain.Commands;

namespace SimpleCqrs.Inventory.Domain;

public abstract record InventoryItemCommand : ICommand
{
    private protected InventoryItemCommand(string? aggregateId) =>
        AggregateId = aggregateId ?? InventoryItemId.Generate();
    
    public string AggregateId { get; }
}