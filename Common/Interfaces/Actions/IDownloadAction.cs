using System.IO;

namespace Common.Interfaces.Actions
{
    public interface IDownloadAction : IAction
    {
        Stream Download(string uri);
    }
}
