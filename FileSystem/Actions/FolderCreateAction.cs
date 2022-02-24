using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using System.IO;

namespace FileSystem.Actions
{
    public class FolderCreateAction : ICreateAction
    {
        protected readonly IResourceFactory factory;
        public ActionType ActionType => ActionType.Create;

        public FolderCreateAction(IResourceFactory resourceFactory)
        {
            factory = resourceFactory;
        }
        public UnifiedResource Create(string url, string name)
        {
            var path = Path.Combine(url, name);
            var idx = 1;
            while (Directory.Exists(path))
            {
                name = $"{name}({idx++})";
                path = Path.Combine(url, name);
            }

            var di = Directory.CreateDirectory(path);

            return factory.Create(di.FullName);
        }
    }
}
