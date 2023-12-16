#if NET8_0_OR_GREATER
using System.Collections.Frozen;
#endif
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics;

namespace Sg.ActionDispatcher;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ActionRoutes
{
    private static readonly Dictionary<Type, List<ActionMethod>> _actions = [];
    private static readonly Dictionary<Type, ActionDeclaringTypeConstructor> _types = [];

#if NET8_0_OR_GREATER
    private static FrozenDictionary<Type, List<ActionMethod>>? _actionsFrozen;
    private static FrozenDictionary<Type, ActionDeclaringTypeConstructor>? _typesFrozen;
#endif

    private static bool _readonly = false;

    public static IReadOnlyCollection<Type> GetInstance() => _types.Keys;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void RegisterType(Type handlerType, Type[] parameters, Func<object?[], object> createInstance)
    {

        if (_readonly)
        {
            //don't throw exception
            return;
        }
        _types.TryAdd(handlerType, new(parameters, createInstance));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void RegisterMethod(Type[] parameters, Func<object?[], Task> invoker)
    {
        if (_readonly)
        {
            //don't throw exception
            return;
        }

        Debug.Assert(parameters.Length >= 2);

        var actionType = parameters[1];

        Debug.Assert(actionType != null);

        if (_actions.TryGetValue(actionType, out var methods))
        {
            methods.Add(new ActionMethod(parameters, invoker));
        }
        else
        {
            _actions.Add(actionType, [new ActionMethod(parameters, invoker)]);
        }
    }

    internal static void MakeReadOnly()
    {
        if (!_readonly)
        {
#if NET8_0_OR_GREATER
            _actionsFrozen = _actions.ToFrozenDictionary(x => x.Key, x => x.Value);
            _typesFrozen = _types.ToFrozenDictionary();
#endif
            _readonly = true;
        }
    }

    internal static ActionDeclaringTypeConstructor GetActionDeclaringTypeConstructor(Type instanceType)
    {
#if NET8_0_OR_GREATER
        return _typesFrozen![instanceType];
#else
        return _types[instanceType];
#endif
    }

    internal static IReadOnlyList<ActionMethod> GetActionMethod(Type actionType)
    {
#if NET8_0_OR_GREATER
        return _actionsFrozen![actionType];
#else
        return _actions[actionType];
#endif
    }
}
