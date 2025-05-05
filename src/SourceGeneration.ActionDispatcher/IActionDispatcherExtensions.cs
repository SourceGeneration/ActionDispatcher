namespace SourceGeneration.ActionDispatcher;

public static class IActionDispatcherExtensions
{
    public static void Dispatch(this IActionDispatcher dispatcher, object action, CancellationToken cancellationToken = default) => dispatcher.Dispatch(action, null, cancellationToken);
    public static Task DispatchAsync(this IActionDispatcher dispatcher, object action, CancellationToken cancellationToken = default) => dispatcher.DispatchAsync(action, null, cancellationToken);

    public static void Dispatch<TAction>(this IActionDispatcher dispatcher, CancellationToken cancellationToken = default) where TAction : new() => dispatcher.Dispatch(new TAction(), null, cancellationToken);
    public static Task DispatchAsync<TAction>(this IActionDispatcher dispatcher, CancellationToken cancellationToken = default) where TAction : new() => dispatcher.DispatchAsync(new TAction(), null, cancellationToken);

    public static void Dispatch<TAction>(this IActionDispatcher dispatcher, ActionDispatchOptions? options, CancellationToken cancellationToken = default) where TAction : new() => dispatcher.Dispatch(new TAction(), options, cancellationToken);
    public static Task DispatchAsync<TAction>(this IActionDispatcher dispatcher, ActionDispatchOptions? options, CancellationToken cancellationToken = default) where TAction : new() => dispatcher.DispatchAsync(new TAction(), options, cancellationToken);

}