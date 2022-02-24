using Common.Enums;
using Common.Interfaces.Actions;
using System.IO;

namespace FileSystem.Actions
{
    public class FileDeleteAction : IDeleteAction
    {
        public ActionType ActionType => ActionType.Delete;

        public void Delete(string url)
        {
            File.Delete(url);
        }
    }
}
