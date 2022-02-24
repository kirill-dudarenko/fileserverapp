using Common.Enums;
using Common.Interfaces.Actions;
using System.IO;

namespace FileSystem.Actions
{
    public class FolderDeleteAction : IDeleteAction
    {
        public ActionType ActionType => ActionType.Delete;

        public void Delete(string url)
        {
            Directory.Delete(url, true);
        }
    }
}
