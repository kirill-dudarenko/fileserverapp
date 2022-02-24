using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using System.Collections.Generic;

namespace FileSystem.Resources
{
    public class UnknownResource : UnifiedResource
    {
        public UnknownResource(string url, IResourceFactory resourceFactory)
            :base(url, ResourceType.Unknown.ToString(), resourceFactory)
        {
            Type = ResourceType.Unknown;
        }

        protected override List<IAction> SetActions()
        {
            return new List<IAction>();
        }
    }
}
