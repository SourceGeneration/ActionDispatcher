namespace SourceGeneration.ActionDispatcher;

public static class IActionSubscriberExtensions
{
    public static IDisposable Subscribe(this IActionSubscriber actionSubscriber, object? subscriber, ActionDispatchStatus status, Action<object, Exception?> callback) => actionSubscriber.Subscribe<object>(subscriber, status, callback);

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, ActionDispatchStatus status, Action<TAction> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, status, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, ActionDispatchStatus status, Action callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, status, (TAction action, Exception? ex) => callback());

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, Action<TAction, Exception?> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, ActionDispatchStatus.RanToCompletion, callback!);
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, Action<TAction> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, Action callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback());

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, ActionDispatchStatus status, Action<TAction, Exception?> callback) where TAction : notnull => actionSubscriber.Subscribe(null, status, callback);
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, ActionDispatchStatus status, Action<TAction> callback) where TAction : notnull => actionSubscriber.Subscribe(null, status, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, ActionDispatchStatus status, Action callback) where TAction : notnull => actionSubscriber.Subscribe(null, status, (TAction action, Exception? ex) => callback());

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, Action<TAction, Exception?> callback) where TAction : notnull => actionSubscriber.Subscribe(null, ActionDispatchStatus.RanToCompletion, callback!);
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, Action<TAction> callback) where TAction : notnull => actionSubscriber.Subscribe(null, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, Action callback) where TAction : notnull => actionSubscriber.Subscribe(null, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback());



    public static IDisposable Subscribe(this IActionSubscriber actionSubscriber, object? subscriber, ActionDispatchStatus status, Func<object, Exception?, Task> callback) => actionSubscriber.Subscribe<object>(subscriber, status, callback);

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, ActionDispatchStatus status, Func<TAction, Task> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, status, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, ActionDispatchStatus status, Func<Task> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, status, (TAction action, Exception? ex) => callback());

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, Func<TAction, Exception?, Task> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, ActionDispatchStatus.RanToCompletion, callback!);
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, Func<TAction, Task> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, object? subscriber, Func<Task> callback) where TAction : notnull => actionSubscriber.Subscribe(subscriber, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback());

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, ActionDispatchStatus status, Func<TAction, Exception?, Task> callback) where TAction : notnull => actionSubscriber.Subscribe(null, status, callback);
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, ActionDispatchStatus status, Func<TAction, Task> callback) where TAction : notnull => actionSubscriber.Subscribe(null, status, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, ActionDispatchStatus status, Func<Task> callback) where TAction : notnull => actionSubscriber.Subscribe(null, status, (TAction action, Exception? ex) => callback());

    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, Func<TAction, Exception?, Task> callback) where TAction : notnull => actionSubscriber.Subscribe(null, ActionDispatchStatus.RanToCompletion, callback!);
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, Func<TAction, Task> callback) where TAction : notnull => actionSubscriber.Subscribe(null, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback(action));
    public static IDisposable Subscribe<TAction>(this IActionSubscriber actionSubscriber, Func<Task> callback) where TAction : notnull => actionSubscriber.Subscribe(null, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback());

}
