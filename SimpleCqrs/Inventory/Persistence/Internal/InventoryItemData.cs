using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Infra.Persistence;

namespace SimpleCqrs.Inventory.Persistence.Internal;

internal class InventoryItemData : 
    Persistent<InventoryItemData>
{
    public Guid Id { get; private set; }
    
    public Guid CatalogId { get; set; }
    
    public InventoryItemStatus Status { get; set; }

    protected override IIdentifier Identifier
    {
        get => InventoryItemId.Parse(Id);
        set => Id = (InventoryItemId)value;
    }
}