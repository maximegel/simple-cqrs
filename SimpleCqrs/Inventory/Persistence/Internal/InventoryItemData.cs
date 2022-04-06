using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Shared.Domain;
using SimpleCqrs.Shared.Infra.Persistence;

namespace SimpleCqrs.Inventory.Persistence.Internal;

internal class InventoryItemData 
{
    public Guid Id { get; set; }
    
    public Guid CatalogId { get; set; }
    
    public InventoryItemStatus Status { get; set; }
    
    public string? StorageLocation { get; set; }
}