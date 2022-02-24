using Common.Enums;
using Common.Interfaces.Actions;
using Common.Interfaces.MetaData;
using System.IO;

namespace FileSystem.Actions
{
    public class FileMetadataAction : IMetadataAction
    {
        public ActionType ActionType => ActionType.Metadata;

        public IMetadata GetMetadata(string url)
        {
            var size = new FileInfo(url).Length;
            return new Metadata(size);
        }
    }
}
