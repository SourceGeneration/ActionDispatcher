namespace Sg.ActionDispatcher;

internal sealed class ActionMethod(Type[] parameters, Func<object?[], Task> invoker)
{
    public readonly Type[] Parameters = parameters;
    public readonly Func<object?[], Task> InvokeAsync = invoker;
}

internal sealed class ActionDeclaringTypeConstructor(Type[] parameters, Func<object?[], object> invoker)
{
    public readonly Type[] Parameters = parameters;
    public readonly Func<object?[], object> InvokeAsync = invoker;
}