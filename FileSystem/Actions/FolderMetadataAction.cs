using Common.Enums;
using Common.Interfaces.Actions;
using Common.Interfaces.MetaData;
using System.IO;

namespace FileSystem.Actions
{
    public class FolderMetadataAction : IMetadataAction
    {
        public ActionType ActionType => ActionType.Metadata;

        public IMetadata GetMetadata(string url)
        {
            var size = GetDirectorySize(new DirectoryInfo(url));
            return new Metadata(size);
        }

        private long GetDirectorySize(DirectoryInfo folder)
        {
            long size = 0;

            foreach (var file in folder.GetFiles())
            {
                size += file.Length;
            }

            foreach (var subfolder in folder.GetDirectories())
            {
                size += GetDirectorySize(subfolder);
            }

            return size;
        }
    }
}
