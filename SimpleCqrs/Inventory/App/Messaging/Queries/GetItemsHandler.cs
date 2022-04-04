using SimpleCqrs.Inventory.App.ReadModel;
using SimpleCqrs.Shared.App.Messaging.Queries;

namespace SimpleCqrs.Inventory.App.Messaging.Queries;

public class GetItemsHandler : QueryHandler<GetItems, GetItemsView>
{
    private readonly InventoryReadModel _readModel;

    public GetItemsHandler(InventoryReadModel readModel) => 
        _readModel = readModel;

    protected override async Task<GetItemsView> Handle(
        GetItems query, 
        CancellationToken cancellationToken)
    {
        var (limit, offset) = query;

        var inventoryItems = await _readModel.InventoryItems
            .Take(limit)
            .Skip(offset)
            .ToArrayAsync(cancellationToken);

        var catalogItems = await _readModel.CatalogItems.FindRange(
            inventoryItems.Select(i => i.CatalogId), 
            cancellationToken);

        var values = inventoryItems
            .Join(
                catalogItems,
                inventoryItem => inventoryItem.CatalogId,
                catalogItem => catalogItem.Id,
                (inventoryItem, catalogItem) => new GetItemsViewValue
                {
                    Id = inventoryItem.Id,
                    CatalogId = catalogItem.Id,
                    Model = catalogItem.Model,
                    Category = catalogItem.Category
                })
            .OrderBy(i => i.Model)
            .ToArray();
        
        var totalCount = await _readModel.InventoryItems.CountAsync(
            cancellationToken);
        
        return new GetItemsView
        {
            Values = values,
            Count = values.Length,
            TotalCount = totalCount
        };
    }
}