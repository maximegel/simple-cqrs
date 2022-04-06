namespace SimpleCqrs.Inventory.App.Processing.Services;

public interface IOrderService
{
    Task MarkAsDelivered(
        string orderId,
        CancellationToken cancellationToken = default);
}