using Microsoft.Extensions.DependencyInjection;
using System.Runtime.ExceptionServices;

namespace SourceGeneration.ActionDispatcher;

internal class ActionDispatcher(IServiceProvider services, IActionNotifier notifier) : IActionDispatcher
{
    public async void Dispatch(object action, CancellationToken cancellationToken = default) => await DispatchAsync(action, false, cancellationToken);

    public Task DispatchAsync(object action, CancellationToken cancellationToken = default) => DispatchAsync(action, true, cancellationToken);

    private async Task DispatchAsync(object action, bool throwException, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(action, nameof(action));

        notifier.Notify(ActionDispatchStatus.WaitingToDispatch, action);

        try
        {
            await DispatchAsyncCore(action, cancellationToken);
        }
        catch (OperationCanceledException ex)
        {
            notifier.Notify(ActionDispatchStatus.Canceled, action, ex);
            return;
        }
        catch (Exception ex)
        {
            notifier.Notify(ActionDispatchStatus.Faulted, action, ex);

            if (throwException)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            return;
        }

        notifier.Notify(ActionDispatchStatus.Successed, action);
    }

    private Task DispatchAsyncCore(object action, CancellationToken cancellationToken)
    {
        var methods = ActionRoutes.GetActionMethod(action.GetType());
        Task[] tasks = new Task[methods.Count];

        Dictionary<Type, object> injects = [];

        for (int i = 0; i < methods.Count; i++)
        {
            ActionMethod method = methods[i];
            var parameters = method.Parameters;
            object?[] arguments = new object?[parameters.Length];

            arguments[0] = parameters[0] == null ? null : CreateInstance(parameters[0]);
            arguments[1] = action;

            for (int j = 2; j < parameters.Length; j++)
            {
                var type = parameters[j];
                if (type == typeof(CancellationToken))
                {
                    arguments[j] = cancellationToken;
                }
                else
                {
                    arguments[j] = GetRequiredService(type);
                }
            }

            tasks[i] = method.InvokeAsync(arguments);
        }

        return Task.WhenAll(tasks);

        object GetRequiredService(Type serviceType)
        {
            if (injects.TryGetValue(serviceType, out var service))
                return service;

            service = services.GetRequiredService(serviceType);
            injects.Add(serviceType, service);

            return service;
        }

        object CreateInstance(Type instanceType)
        {
            if (injects.TryGetValue(instanceType, out var instance))
                return instance;

            instance = services.GetService(instanceType);
            if (instance == null)
            {
                var definition = ActionRoutes.GetActionDeclaringTypeConstructor(instanceType);

                object[] arguments = new object[definition.Parameters.Length];
                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = GetRequiredService(definition.Parameters[i]);
                }
                instance = definition.InvokeAsync(arguments);
            }

            injects.Add(instanceType, instance);
            return instance;
        }

    }

}