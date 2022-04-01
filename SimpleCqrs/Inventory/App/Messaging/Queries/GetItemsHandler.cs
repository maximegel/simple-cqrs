using SimpleCqrs.Inventory.App.Projection;
using SimpleCqrs.Shared.App.Messaging.Queries;

namespace SimpleCqrs.Inventory.App.Messaging.Queries;

public class GetItemsHandler : QueryHandler<GetItems, GetItemsView>
{
    private readonly IInventoryReadModel _readModel;

    public GetItemsHandler(IInventoryReadModel readModel) => 
        _readModel = readModel;

    protected override async Task<GetItemsView> Handle(GetItems query, CancellationToken cancellationToken)
    {
        var (limit, offset) = query;

        var values = await _readModel.InventoryItems
            .OrderBy(i => i.Model)
            .Take(limit)
            .Skip(offset)
            .Select(i => new GetItemsViewValue
            {
                Id = i.Id.ToString(),
                Model = i.Model,
                Category = i.Category
            })
            .ToArrayAsync(cancellationToken);
        
        var totalCount = await _readModel.InventoryItems.CountAsync(cancellationToken);
        
        return new GetItemsView
        {
            Values = values,
            Count = values.Length,
            TotalCount = totalCount
        };
    }
}