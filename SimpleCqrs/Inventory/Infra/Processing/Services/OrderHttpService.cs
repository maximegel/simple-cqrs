using SimpleCqrs.Inventory.App.Processing.Services;

namespace SimpleCqrs.Inventory.Infra.Processing.Services;

public class OrderHttpService : IOrderService
{
    public Task MarkAsDelivered(
        string orderId, 
        CancellationToken cancellationToken = default)
    {
        // Invoke a real HTTP service here...
        return Task.CompletedTask;
    }
}