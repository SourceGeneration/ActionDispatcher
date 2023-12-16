using Microsoft.Extensions.DependencyInjection;

namespace Sg.ActionDispatcher;

public static class SgActionDispatcherServiceCollectionExtensions
{
    public static IServiceCollection AddActionDispatcher(this IServiceCollection services)
    {
        services.AddScoped<ActionSubscriber>();
        services.AddScoped<IActionSubscriber>(p => p.GetRequiredService<ActionSubscriber>());
        services.AddScoped<IActionNotifier>(p => p.GetRequiredService<ActionSubscriber>());
        services.AddScoped<IActionDispatcher, ActionDispatcher>();
        ActionRoutes.MakeReadOnly();
        return services;
    }
}
