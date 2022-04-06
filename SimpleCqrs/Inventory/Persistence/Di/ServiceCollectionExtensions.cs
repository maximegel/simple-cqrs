using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Shared.App.Persistence.Events;
using SimpleCqrs.Shared.Infra.Persistence.Core.Events;

namespace SimpleCqrs.Inventory.Persistence.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryPersistence(
        this IServiceCollection services)
    {
        services.AddScoped(
            provider =>
            {
                var publisher = provider.GetRequiredService<IEventPublisher>();
                var dbContext = provider.GetRequiredService<InventorySqlContext>();
                return new InventoryItemSqlRepository(dbContext)
                    .UseImmediatePublisher(publisher);
            });

        services.AddDbContext<InventorySqlContext>(
            options => options.UseSqlite("DataSource=file::memory:?cache=shared"),
            // Hack for SQLite in-memory.
            ServiceLifetime.Singleton);
        
        return services;
    }

}