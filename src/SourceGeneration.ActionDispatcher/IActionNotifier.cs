namespace SourceGeneration.ActionDispatcher;

public interface IActionNotifier
{
    void Notify(object action);
    void Notify(object action, Exception exception);
    void Notify(ActionDispatchStatus status, object action, Exception? exception = null);
}
