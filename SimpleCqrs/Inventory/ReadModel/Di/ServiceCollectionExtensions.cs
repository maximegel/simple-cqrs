using SimpleCqrs.Inventory.App.ReadModel.Projections;
using SimpleCqrs.Inventory.App.ReadModel.Services;
using SimpleCqrs.Inventory.ReadModel.Projections;
using SimpleCqrs.Inventory.ReadModel.Services;

namespace SimpleCqrs.Inventory.ReadModel.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryReadModel(
        this IServiceCollection services)
    {
        // Projections:
        services.AddScoped<IInventoryItemsProjection, InventoryItemsSqlProjection>();
        // Services:
        services.AddScoped<ICatalogItemsQueryService, CatalogItemsHttpQueryService>();
        
        return services;
    }
}