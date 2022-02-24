namespace Common.Interfaces.Actions
{
    public interface IRenameAction : IAction
    {
        string Rename(string url, string newName);
    }
}
