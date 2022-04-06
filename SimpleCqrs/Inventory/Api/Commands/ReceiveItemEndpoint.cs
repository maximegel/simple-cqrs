using Microsoft.AspNetCore.Mvc;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Messaging.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace SimpleCqrs.Inventory.Api.Commands;

[ApiController]
public class ReceiveItemEndpoint : ControllerBase
{
    private readonly ICommandBus _bus;
    
    public ReceiveItemEndpoint(ICommandBus bus) => 
        _bus = bus;

    [HttpPost("api/inventory/receive-item")]
    [SwaggerOperation(
        Summary = "Receive item",
        Description = "Receive a previously ordered inventory item.",
        OperationId = nameof(ReceiveItem),
        Tags = new[] { "Inventory" })]
    public async Task<ActionResult> Invoke(ReceiveItemDto dto)
    {
        var (id, catalogId, orderId) = dto;
        await _bus.Send(new ReceiveItem(id, catalogId, orderId));
        return Accepted();
    }
}

public record ReceiveItemDto(
    string? Id, 
    string CatalogId,
    string OrderId);