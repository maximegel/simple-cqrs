using SimpleCqrs.Inventory.App.ReadModel;

namespace SimpleCqrs.Inventory.App.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryApp(
        this IServiceCollection services)
    {
        services.AddScoped<InventoryReadModel>();
        
        return services;
    }
}