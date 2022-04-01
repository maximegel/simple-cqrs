using Microsoft.AspNetCore.Mvc;
using SimpleCqrs.Inventory.App.Messaging.Queries;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Messaging.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SimpleCqrs.Inventory.Api.Queries;

[ApiController]
public class GetItemsEndpoint : ControllerBase
{
    private readonly IQueryBus _bus;
    
    public GetItemsEndpoint(IQueryBus bus) => 
        _bus = bus;

    [HttpGet("api/inventory/items")]
    [SwaggerOperation(
        Summary = "Get items",
        Description = "Get a list of inventory items.",
        OperationId = nameof(ReceiveItem),
        Tags = new[] { "Inventory" })]
    public async Task<ActionResult<GetItemsView>> Invoke()
    {
        var result = await _bus.Send(new GetItems());
        return Ok(result);
    }
}