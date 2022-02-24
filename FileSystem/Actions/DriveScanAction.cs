using Common.Interfaces;

namespace FileSystem.Actions
{
    public class DriveScanAction : FolderScanAction
    {
        public DriveScanAction(IResourceFactory resourceFactory):base(resourceFactory)
        {}
    }
}
