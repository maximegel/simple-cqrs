using Microsoft.AspNetCore.Mvc;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Messaging.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace SimpleCqrs.Inventory.Api.Commands;

[ApiController]
public class RelocateItemEndpoint : ControllerBase
{
    private readonly ICommandBus _bus;
    
    public RelocateItemEndpoint(ICommandBus bus) => 
        _bus = bus;

    [HttpPut("api/inventory/{id}/relocate-item")]
    [SwaggerOperation(
        Summary = "Relocate item",
        Description = "Change the storage location of an inventory item.",
        OperationId = nameof(RelocateItem),
        Tags = new[] { "Inventory" })]
    public async Task<ActionResult> Invoke(string id, RelocateItemDto dto)
    {
        await _bus.Send(new RelocateItem(id, dto.StorageLocation));
        return Accepted();
    }
}

public record RelocateItemDto(
    string StorageLocation);