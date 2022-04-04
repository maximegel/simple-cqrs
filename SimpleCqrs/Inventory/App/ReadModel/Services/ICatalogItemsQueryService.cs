namespace SimpleCqrs.Inventory.App.ReadModel.Services;

public interface ICatalogItemsQueryService
{
    Task<IEnumerable<CatalogItemModel>> FindRange(
        IEnumerable<string> ids,
        CancellationToken cancellationToken = default);
}

public record CatalogItemModel
{
    public string Id { get; init; } = null!;
    
    public string Model { get; init; } = null!;

    public string Category { get; init; } = null!;
}