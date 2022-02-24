using Common.Enums;
using Common.Interfaces.Actions;
using System.IO;

namespace FileSystem.Actions
{
    public class FileDownloadAction : IDownloadAction
    {
        public ActionType ActionType => ActionType.Download;

        public Stream Download(string uri)
        {
            return new FileStream(uri, FileMode.Open, FileAccess.Read);
        }
    }
}
