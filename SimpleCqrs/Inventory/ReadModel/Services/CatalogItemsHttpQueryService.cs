using SimpleCqrs.Inventory.App.ReadModel.Services;

namespace SimpleCqrs.Inventory.ReadModel.Services;

public class CatalogItemsHttpQueryService :
    ICatalogItemsQueryService
{
    public Task<IEnumerable<CatalogItemModel>> FindRange(
        IEnumerable<string> ids, 
        CancellationToken cancellationToken = default)
    {
        // Invoke a real HTTP service here...
        var result = ids.Select((id, i) => new CatalogItemModel
        {
            Id = id,
            Model = $"Salomon X Ultra {i + 1}",
            Category = "Shoes"
        });
        return Task.FromResult(result);
    }
}