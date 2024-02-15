using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SourceGeneration.ActionDispatcher;

public static class SgActionDispatcherServiceCollectionExtensions
{
    public static IServiceCollection AddActionDispatcher(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped, Action<ActionDispatcherOptions>? optionsAction = null)
    {
        ActionDispatcherOptions options = new();
        optionsAction?.Invoke(options);

        services.AddSingleton(options);
        services.TryAdd(new ServiceDescriptor(typeof(ActionSubscriber), typeof(ActionSubscriber), serviceLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IActionSubscriber), p => p.GetRequiredService<ActionSubscriber>(), serviceLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IActionNotifier), p => p.GetRequiredService<ActionSubscriber>(), serviceLifetime));
        services.TryAdd(new ServiceDescriptor(typeof(IActionDispatcher), typeof(ActionDispatcher), serviceLifetime));
        return services;
    }

    public static IServiceCollection AddActionDispatcher(this IServiceCollection services, Action<ActionDispatcherOptions> optionsAction) => AddActionDispatcher(services, serviceLifetime: ServiceLifetime.Scoped, optionsAction);
}
