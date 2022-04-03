using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Inventory.Persistence.Internal;
using SimpleCqrs.Shared.App.Persistence;
using SimpleCqrs.Shared.Infra.Persistence;
using SimpleCqrs.Shared.Infra.Persistence.EntityFramework;

namespace SimpleCqrs.Inventory.Persistence.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryPersistence(
        this IServiceCollection services)
    {
        services.AddScoped<IRepository<IInventoryItem>>(
            provider => provider.GetRequiredService<InventorySqlContext>()
                .AsRepository<InventoryItemData>()
                .UseMapper(new InventoryItemSqlMapper()));

        services.AddDbContext<InventorySqlContext>(options => 
            options.UseSqlite("DataSource=file::memory:?cache=shared"));
        
        return services;
    }
}