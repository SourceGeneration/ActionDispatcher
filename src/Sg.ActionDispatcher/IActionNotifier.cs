namespace SourceGeneration.ActionDispatcher;

public interface IActionNotifier
{
    void Notify(ActionDispatchStatus status, object action, Exception? exception = null);
}
