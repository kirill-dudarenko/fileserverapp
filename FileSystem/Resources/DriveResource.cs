using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using FileSystem.Actions;
using System.Collections.Generic;

namespace FileSystem.Resources
{
    public class DriveResource : UnifiedResource
    {
        public DriveResource(string url, string name, IResourceFactory resourceFactory)
            :base(url, name, resourceFactory)
        {
            Type = ResourceType.Drive;
        }

        protected override List<IAction> SetActions()
        {
            return new List<IAction> 
            { 
                new DriveScanAction(factory)
            };
        }
    }
}
