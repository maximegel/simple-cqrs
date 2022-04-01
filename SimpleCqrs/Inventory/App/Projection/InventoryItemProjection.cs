namespace SimpleCqrs.Inventory.App.Projection;

public record InventoryItemProjection
{
    public Guid Id { get; init; }

    public string Model { get; init; } = null!;

    public string Category { get; init; } = null!;
}