namespace SourceGeneration.ActionDispatcher;

public interface IActionDispatcher
{
    void Dispatch(object action, CancellationToken cancellationToken = default);
    Task DispatchAsync(object action, CancellationToken cancellationToken = default);

    void Dispatch<TAction>(CancellationToken cancellationToken = default) where TAction : new() => DispatchAsync(new TAction(), cancellationToken);
    Task DispatchAsync<TAction>(CancellationToken cancellationToken = default) where TAction : new() => DispatchAsync(new TAction(), cancellationToken);
}
