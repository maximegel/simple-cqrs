using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleCqrs.Inventory.App.Processing.Services;
using SimpleCqrs.Inventory.App.ReadModel;
using SimpleCqrs.Inventory.App.ReadModel.Projections;
using SimpleCqrs.Inventory.App.ReadModel.Services;
using SimpleCqrs.Inventory.Infra.Persistence;
using SimpleCqrs.Inventory.Infra.Processing.Services;
using SimpleCqrs.Inventory.Infra.ReadModel.Projections;
using SimpleCqrs.Inventory.Infra.ReadModel.Services;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Messaging.Queries;
using SimpleCqrs.Shared.App.Persistence.Events;
using SimpleCqrs.Shared.Infra.Messaging.MediatR;
using SimpleCqrs.Shared.Infra.Persistence.Core.Events;

namespace SimpleCqrs.Inventory.App.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInventoryApp(
        this IServiceCollection services)
    {
        // ### Messaging ###
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<ICommandBus, MediatorBus>();
        services.AddScoped<IQueryBus, MediatorBus>();
        services.AddScoped<IEventPublisher, MediatorEventPublisher>();
        
        // ### Persistence ###
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
        
        // ### Processing ###
        services.AddScoped<IOrderService, OrderHttpService>();
        
        // ### ReadModel ###
        services.AddScoped<InventoryReadModel>();
        services.AddScoped<IInventoryItemsProjection, InventoryItemsSqlProjection>();
        services.AddScoped<ICatalogItemsQueryService, CatalogItemsHttpQueryService>();
        
        return services;
    }
}