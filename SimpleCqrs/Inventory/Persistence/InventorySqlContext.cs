using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.Persistence.Internal;

namespace SimpleCqrs.Inventory.Persistence;

public class InventorySqlContext : DbContext
{
    public InventorySqlContext(DbContextOptions<InventorySqlContext> options)
        : base(options)
    {
    }
    
    internal DbSet<InventoryItemData> InventoryItems { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryItemData>(entity =>
        {
            entity.ToTable("InventoryItem");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
        });
    }
}