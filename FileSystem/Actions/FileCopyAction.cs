using Common.Enums;
using Common.Interfaces.Actions;
using System.IO;

namespace FileSystem.Actions
{
    public class FileCopyAction : ICopyAction
    {
        public ActionType ActionType => ActionType.Copy;

        public string Copy(string url)
        {
            var dir = Path.GetDirectoryName(url);
            var file = Path.GetFileName(url);
            var newFile = "Copy of " + file;
            var newUrl = Path.Combine(dir, newFile);

            File.Copy(url, newUrl);

            return newUrl;
        }
    }
}
