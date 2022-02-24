using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using FileSystem.Actions;
using System.Collections.Generic;

namespace FileSystem.Resources
{
    public class RootResource : UnifiedResource
    {
        public RootResource(IResourceFactory resourceFactory)
            :base(null, ResourceType.Root.ToString(), resourceFactory)
        {
            Type = ResourceType.Root;
        }
        protected override List<IAction> SetActions()
        {
            return new List<IAction>
            {
                new RootScanAction(factory)
            };
        }
    }
}
