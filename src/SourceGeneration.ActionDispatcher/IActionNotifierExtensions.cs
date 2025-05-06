namespace SourceGeneration.ActionDispatcher;

public static class IActionNotifierExtensions
{
    public static void Notify(this IActionNotifier notifier, object action) => notifier.Notify(ActionDispatchStatus.Successed, action);
    public static void Notify(this IActionNotifier notifier, object action, Exception exception) => notifier.Notify(ActionDispatchStatus.Faulted, action, exception);
}