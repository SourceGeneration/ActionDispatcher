﻿namespace SourceGeneration.ActionDispatcher;

public interface IActionSubscriber
{
    void Unsubscribe(object subscriber);

    IDisposable Subscribe<TAction>(object? subscriber, ActionDispatchStatus status, Action<TAction, Exception?> callback) where TAction : notnull;
    IDisposable Subscribe(object? subscriber, ActionDispatchStatus status, Type[] actionTypes, Action<object, Exception?> callback);

    IDisposable Subscribe(object? subscriber, ActionDispatchStatus status, Action<object, Exception?> callback) => Subscribe<object>(subscriber, status, callback);

    IDisposable Subscribe<TAction>(object? subscriber, ActionDispatchStatus status, Action<TAction> callback) where TAction : notnull => Subscribe(subscriber, status, (TAction action, Exception? ex) => callback(action));
    IDisposable Subscribe<TAction>(object? subscriber, ActionDispatchStatus status, Action callback) where TAction : notnull => Subscribe(subscriber, status, (TAction action, Exception? ex) => callback());

    IDisposable Subscribe<TAction>(object? subscriber, Action<TAction, Exception?> callback) where TAction : notnull => Subscribe(subscriber, ActionDispatchStatus.RanToCompletion, callback!);
    IDisposable Subscribe<TAction>(object? subscriber, Action<TAction> callback) where TAction : notnull => Subscribe(subscriber, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback(action));
    IDisposable Subscribe<TAction>(object? subscriber, Action callback) where TAction : notnull => Subscribe(subscriber, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback());

    IDisposable Subscribe<TAction>(ActionDispatchStatus status, Action<TAction, Exception?> callback) where TAction : notnull => Subscribe(null, status, callback);
    IDisposable Subscribe<TAction>(ActionDispatchStatus status, Action<TAction> callback) where TAction : notnull => Subscribe(null, status, (TAction action, Exception? ex) => callback(action));
    IDisposable Subscribe<TAction>(ActionDispatchStatus status, Action callback) where TAction : notnull => Subscribe(null, status, (TAction action, Exception? ex) => callback());

    IDisposable Subscribe<TAction>(Action<TAction, Exception?> callback) where TAction : notnull => Subscribe(null, ActionDispatchStatus.RanToCompletion, callback!);
    IDisposable Subscribe<TAction>(Action<TAction> callback) where TAction : notnull => Subscribe(null, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback(action));
    IDisposable Subscribe<TAction>(Action callback) where TAction : notnull => Subscribe(null, ActionDispatchStatus.Successed, (TAction action, Exception? ex) => callback());
}