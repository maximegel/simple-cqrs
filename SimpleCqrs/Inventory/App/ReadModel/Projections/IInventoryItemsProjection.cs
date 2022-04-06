using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Shared.Infra.ReadModel.Projections;

namespace SimpleCqrs.Inventory.App.ReadModel.Projections;

public interface IInventoryItemsProjection : 
    IQueryableProjection<InventoryItemModel>
{
}

public record InventoryItemModel
{
    public string Id { get; init; } = null!;

    public string CatalogId { get; init; } = null!;

    public InventoryItemStatus Status { get; init; }
    
    public string? StorageLocation { get; init; }
}