using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using FileSystem.Actions;
using System.Collections.Generic;

namespace FileSystem.Resources
{
    public class FolderResource : UnifiedResource
    {
        public FolderResource(string url, string name, IResourceFactory resourceFactory)
            :base(url, name, resourceFactory)
        {
            Type = ResourceType.Folder;
        }

        protected override List<IAction> SetActions()
        {
            return new List<IAction>
            {
                new FolderCopyAction(),
                new FolderDeleteAction(),
                new FolderRenameAction(),
                new FolderCreateAction(factory),
                new FolderScanAction(factory),
                new FileUploadAction(factory),
                new FolderMetadataAction(),
            };
        }
    }
}
