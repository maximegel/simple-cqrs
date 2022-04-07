using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.Infra.Persistence.DataModels;

namespace SimpleCqrs.Inventory.Infra.Persistence;

public class InventorySqlContext : DbContext
{
    public InventorySqlContext(DbContextOptions<InventorySqlContext> options)
        : base(options)
    {
    }
    
    public DbSet<InventoryItemData> InventoryItems { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryItemData>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
        });
    }
}