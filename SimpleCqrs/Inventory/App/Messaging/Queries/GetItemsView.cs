namespace SimpleCqrs.Inventory.App.Messaging.Queries;

public record GetItemsView
{
    public IEnumerable<GetItemsViewValue> Values { get; init; } = 
        Enumerable.Empty<GetItemsViewValue>();

    public int Count { get; init; }
    
    public int TotalCount { get; init; }
}

public record GetItemsViewValue
{
    public string Id { get; init; } = null!;
    
    public string Model { get; init; } = null!;
    
    public string Category { get; init; } = null!;
}