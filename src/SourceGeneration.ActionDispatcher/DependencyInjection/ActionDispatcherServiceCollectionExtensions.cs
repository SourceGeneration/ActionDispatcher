using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SourceGeneration.ActionDispatcher.Internal;

namespace SourceGeneration.ActionDispatcher;

public static class ActionDispatcherServiceCollectionExtensions
{
    public static IServiceCollection AddActionDispatcher(this IServiceCollection services, ServiceLifetime dispatcherLifetime = ServiceLifetime.Scoped, ServiceLifetime subscriberLifetime = ServiceLifetime.Scoped)
    {
        services.Add(new ServiceDescriptor(typeof(IActionDispatcher), typeof(Internal.ActionDispatcher), dispatcherLifetime));

        services.TryAdd(new ServiceDescriptor(typeof(ActionSubscriber), typeof(ActionSubscriber), subscriberLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IActionSubscriber), p => p.GetRequiredService<ActionSubscriber>(), subscriberLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IActionNotifier), p => p.GetRequiredService<ActionSubscriber>(), subscriberLifetime));

        return services;
    }

    public static IServiceCollection AddActionSubscriber(this IServiceCollection services, ServiceLifetime subscriberLifetime = ServiceLifetime.Scoped)
    {
        services.Add(new ServiceDescriptor(typeof(ActionSubscriber), typeof(ActionSubscriber), subscriberLifetime));
        services.Add(new ServiceDescriptor(typeof(IActionSubscriber), p => p.GetRequiredService<ActionSubscriber>(), subscriberLifetime));
        services.Add(new ServiceDescriptor(typeof(IActionNotifier), p => p.GetRequiredService<ActionSubscriber>(), subscriberLifetime));
        return services;
    }

#if NET8_0_OR_GREATER
    public static IServiceCollection AddKeyedActionSubscriber(this IServiceCollection services, object? serviceKey, ServiceLifetime subscriberLifetime = ServiceLifetime.Scoped)
    {
        services.Add(new ServiceDescriptor(typeof(ActionSubscriber), serviceKey, typeof(ActionSubscriber), subscriberLifetime));
        services.Add(new ServiceDescriptor(typeof(IActionSubscriber), serviceKey, (sp, key) => sp.GetRequiredKeyedService<ActionSubscriber>(key), subscriberLifetime));
        services.Add(new ServiceDescriptor(typeof(IActionNotifier), serviceKey, (sp, key) => sp.GetRequiredKeyedService<ActionSubscriber>(key), subscriberLifetime));
        return services;
    }

#endif

}
