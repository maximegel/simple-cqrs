using SimpleCqrs.Inventory.App.Processing.Services;
using SimpleCqrs.Inventory.Domain.Events;
using SimpleCqrs.Shared.App.Messaging.Events;

namespace SimpleCqrs.Inventory.App.Processing.Handlers.OnItemReceived;

public class MarkOrderAsDeliveredHandler : 
    IEventHandler<IntegrationEvent<ItemReceived>>
{
    private readonly IOrderService _orderService;
    
    public MarkOrderAsDeliveredHandler(IOrderService orderService) => 
        _orderService = orderService;

    public async Task Handle(
        IntegrationEvent<ItemReceived> e, 
        CancellationToken cancellationToken)
    {
        await _orderService.MarkAsDelivered(
            e.Payload.OrderId, 
            cancellationToken);
    }
}