using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Domain.Events;

namespace SimpleCqrs.Inventory.Infra.Persistence.DataModels;

public class InventoryItemData 
{
    public Guid Id { get; init; }
    
    public Guid CatalogId { get; set; }
    
    public InventoryItemStatus Status { get; set; }
    
    public string? StorageLocation { get; set; }
}