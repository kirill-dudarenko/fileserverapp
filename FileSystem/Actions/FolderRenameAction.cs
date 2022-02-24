using Common.Enums;
using Common.Interfaces.Actions;
using System.IO;

namespace FileSystem.Actions
{
    public class FolderRenameAction : IRenameAction
    {
        public ActionType ActionType => ActionType.Rename;

        public string Rename(string url, string newName)
        {
            var dir = Path.GetDirectoryName(url);
            var newUrl = Path.Combine(dir, newName);

            Directory.Move(url, newUrl);

            return newUrl;
        }
    }
}
