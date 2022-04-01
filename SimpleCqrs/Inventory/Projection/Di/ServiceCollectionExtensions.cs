using SimpleCqrs.Inventory.App.Projection;

namespace SimpleCqrs.Inventory.Projection.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryProjection(
        this IServiceCollection services)
    {
        services.AddScoped<IInventoryReadModel, InventorySqlReadModel>();
        
        return services;
    }
}