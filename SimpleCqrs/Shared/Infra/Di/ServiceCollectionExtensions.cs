﻿using System.Reflection;
using MediatR;
using SimpleCqrs.Shared.App.Messaging.Commands;
using SimpleCqrs.Shared.App.Messaging.Events;
using SimpleCqrs.Shared.App.Messaging.Queries;
using SimpleCqrs.Shared.App.Persistence.Events;
using SimpleCqrs.Shared.Infra.Messaging.MediatR;

namespace SimpleCqrs.Shared.Infra.Di;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services, Assembly appAssembly)
    {
        services.AddMediatR(appAssembly);
        services.AddScoped<ICommandBus, MediatorBus>();
        services.AddScoped<IQueryBus, MediatorBus>();
        services.AddScoped<IEventPublisher, MediatorEventPublisher>();
        return services;
    }
}