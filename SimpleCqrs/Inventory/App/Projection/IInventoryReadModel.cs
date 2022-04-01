namespace SimpleCqrs.Inventory.App.Projection;

public interface IInventoryReadModel
{
    IAsyncQueryable<InventoryItemProjection> InventoryItems { get; }
}