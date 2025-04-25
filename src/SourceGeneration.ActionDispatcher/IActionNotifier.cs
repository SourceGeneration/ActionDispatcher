namespace SourceGeneration.ActionDispatcher;

public interface IActionNotifier
{
    void Notify(object action) => Notify(ActionDispatchStatus.Successed, action);
    void Notify(object action, Exception exception) => Notify(ActionDispatchStatus.Faulted, action,exception);
    void Notify(ActionDispatchStatus status, object action, Exception? exception = null);
}
