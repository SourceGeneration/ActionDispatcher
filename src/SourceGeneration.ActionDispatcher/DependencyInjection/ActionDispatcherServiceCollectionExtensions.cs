﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SourceGeneration.ActionDispatcher;

public static class ActionDispatcherServiceCollectionExtensions
{
    public static IServiceCollection AddActionDispatcher(this IServiceCollection services,
        ServiceLifetime dispatcherLifetime = ServiceLifetime.Scoped,
        ServiceLifetime subscriberLifetime = ServiceLifetime.Scoped)
    {
        services.TryAdd(new ServiceDescriptor(typeof(ActionSubscriber), typeof(ActionSubscriber), subscriberLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IActionSubscriber), p => p.GetRequiredService<ActionSubscriber>(), subscriberLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IActionNotifier), p => p.GetRequiredService<ActionSubscriber>(), subscriberLifetime));

        services.TryAdd(new ServiceDescriptor(typeof(IActionDispatcher), typeof(ActionDispatcher), dispatcherLifetime));
        return services;
    }
}
