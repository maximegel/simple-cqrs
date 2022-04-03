namespace SimpleCqrs.Inventory.Domain.Internal.States;

internal record InStock : InventoryItemState
{
    public override InventoryItemStatus Status => InventoryItemStatus.InStock;
}