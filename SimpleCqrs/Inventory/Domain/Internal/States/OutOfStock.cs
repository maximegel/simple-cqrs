namespace SimpleCqrs.Inventory.Domain.Internal.States;

internal record OutOfStock : InventoryItemState
{
    public override InventoryItemStatus Status => InventoryItemStatus.OutOfStock;
    
    public override InventoryItemState Receive() => 
        new InStock();
}