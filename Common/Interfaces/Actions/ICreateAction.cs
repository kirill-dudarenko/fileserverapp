namespace Common.Interfaces.Actions
{
    public interface ICreateAction : IAction
    {
        UnifiedResource Create(string url, string name);
    }
}
