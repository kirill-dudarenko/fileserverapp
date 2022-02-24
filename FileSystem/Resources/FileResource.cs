using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using FileSystem.Actions;
using System.Collections.Generic;


namespace FileSystem.Resources
{
    public class FileResource : UnifiedResource
    {
        public FileResource(string url, string name, IResourceFactory resourceFactory)
            :base(url, name, resourceFactory)
        {
            Type = ResourceType.File;
        }

        protected override List<IAction> SetActions()
        {
            return new List<IAction>
            {
                new FileCopyAction(),
                new FileDeleteAction(),
                new FileRenameAction(),
                new FileDownloadAction(),
                new FileMetadataAction()
            };
        }
    }
}
