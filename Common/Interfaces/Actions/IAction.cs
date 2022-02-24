using Common.Enums;

namespace Common.Interfaces.Actions
{
    public interface IAction
    {
        ActionType ActionType { get; }
    }
}
