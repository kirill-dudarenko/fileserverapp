using Common;
using Common.Interfaces;
using FileSystem.Resources;
using System.IO;

namespace FileSystem
{
    public class FileSystemResourceFactory : IResourceFactory
    {
        public UnifiedResource Create(string uri)
        {
            // let's treat it as root
            if (string.IsNullOrEmpty(uri) || string.Equals(uri, "Root", System.StringComparison.OrdinalIgnoreCase))
            {
                return new RootResource(this);
            }

            var folderInfo = new DirectoryInfo(uri);
            var fileInfo = new FileInfo(uri);

            if (folderInfo.Parent == null)
            {
                // directory attributes is going to be -ve for CD/DVD virtual empty drives
                if (folderInfo.Exists && folderInfo.Attributes != ((FileAttributes)(-1)))
                    return new DriveResource("Root", uri, this);

                return new UnknownResource("Root", this);
            }
            else if (folderInfo.Exists)
            {
                var location = folderInfo.Parent == null ? folderInfo.Root.FullName : folderInfo.Parent.FullName;
                return new FolderResource(location, folderInfo.Name, this);
            }
            else if (fileInfo.Exists)
            {
                return new FileResource(fileInfo.DirectoryName, fileInfo.Name, this);
            }

            return new UnknownResource(uri, this);
        }
    }
}
