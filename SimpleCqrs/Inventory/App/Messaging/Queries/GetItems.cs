using SimpleCqrs.Shared.App.Messaging.Queries;

namespace SimpleCqrs.Inventory.App.Messaging.Queries;

public record GetItems(
    int Limit = 10,
    int Offset = 0) :
    Query<GetItemsView>;
