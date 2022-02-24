using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using System.Collections.Generic;
using System.IO;

namespace FileSystem.Actions
{
    public class RootScanAction : IScanAction
    {
        public ActionType ActionType => ActionType.Scan;
        IResourceFactory factory;

        public RootScanAction(IResourceFactory resourceFactory)
        {
            factory = resourceFactory;
        }

        public IEnumerable<UnifiedResource> GetChildrenResources(string url)
        {
            var ret = new List<UnifiedResource>();

            foreach (var item in Directory.GetLogicalDrives())
            {
                ret.Add(factory.Create(item));
            }

            return ret;
        }
    }
}
