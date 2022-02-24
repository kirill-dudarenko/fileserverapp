using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using System.Collections.Generic;
using System.IO;

namespace FileSystem.Actions
{
    public class FolderScanAction : IScanAction
    {
        protected readonly IResourceFactory factory;
        public FolderScanAction(IResourceFactory resourceFactory)
        {
            factory = resourceFactory;
        }

        public ActionType ActionType => ActionType.Scan;

        public IEnumerable<UnifiedResource> GetChildrenResources(string url)
        {
            var ret = new List<UnifiedResource>();

            foreach (var item in Directory.EnumerateFileSystemEntries(url))
            {
                ret.Add(factory.Create(item));
            }

            return ret;
        }
    }
}
