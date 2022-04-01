using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.Domain;
using SimpleCqrs.Shared.App.Persistence;

namespace SimpleCqrs.Inventory.Persistence.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryPersistence(
        this IServiceCollection services)
    {
        services.AddScoped<IRepository<IInventoryItem>, InventoryItemSqlRepository>();

        services.AddDbContext<InventorySqlContext>(options => 
            options.UseSqlite("DataSource=file::memory:?cache=shared"));
        
        return services;
    }
}