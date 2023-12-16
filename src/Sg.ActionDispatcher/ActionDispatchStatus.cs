namespace Sg.ActionDispatcher;

[Flags]
public enum ActionDispatchStatus
{
    WaitingToDispatch = 1,
    Canceled = 2,
    Successed = 4,
    Faulted = 8,
    RanToCompletion = Successed | Faulted | Canceled,
}