namespace SimpleCqrs.Inventory.Persistence.Internal;

internal record InventoryItemData
{
    public Guid Id { get; init; }
    
    public string Model { get; set; } = null!;
    
    public string Category { get; set; } = null!;
}