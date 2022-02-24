using Common.Enums;
using Common.Interfaces.Actions;
using System.IO;

namespace FileSystem.Actions
{
    public class FolderCopyAction : ICopyAction
    {
        public ActionType ActionType => ActionType.Copy;

        public string Copy(string url)
        {
            var source = new DirectoryInfo(url);
            var dir = Path.GetDirectoryName(url);
            var newUrl = Path.Combine(dir, "Copy of " + source.Name);
            var target = new DirectoryInfo(newUrl);

            XCopy(source, target);

            return newUrl;
        }

        private void XCopy(DirectoryInfo source, DirectoryInfo target)
        {
            if (!Directory.Exists(target.FullName))
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Files go first
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }

            // Recursively go through subfolders
            foreach (var subFolder in source.GetDirectories())
            {
                var nextTarget = target.CreateSubdirectory(subFolder.Name);
                XCopy(subFolder, nextTarget);
            }
        }
    }
}
