using SimpleCqrs.Inventory.App.Messaging.Commands.Base;
using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Commands;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.App.Messaging.Commands;

public class RelocateItemHandler : UpdateItemHandler<RelocateItem>
{
    public RelocateItemHandler(IRepository<IInventoryItem> repository) :
        base(repository)
    {
    }
}