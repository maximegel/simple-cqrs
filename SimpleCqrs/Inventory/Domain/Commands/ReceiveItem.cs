namespace SimpleCqrs.Inventory.Domain.Commands;

public record ReceiveItem(string? AggregateId, string Model, string Category) 
    : InventoryItemCommand(AggregateId)
{
}