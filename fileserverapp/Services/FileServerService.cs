using Common;
using Common.Interfaces;
using Common.Interfaces.Actions;
using Common.Interfaces.MetaData;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fileserverapp.Services
{
    public class FileServerService : IFileServerService
    {
        IResourceFactory resourceFactory;

        public FileServerService(IResourceFactory resourceFactory)
        {
            this.resourceFactory = resourceFactory;
        }

        public UnifiedResource Copy(string url)
        {
            var resource = Get(url);
            var action = resource.GetAction<ICopyAction>();

            if (action != null)
            {
                var newUrl = action.Copy(url);
                resource = Get(newUrl);
            }

            return resource;
        }

        public void Delete(string url)
        {
            var resource = Get(url);
            var action = resource.GetAction<IDeleteAction>();

            action?.Delete(url);
        }

        public UnifiedResource Get(string url)
        {
            return resourceFactory.Create(url);
        }

        public UnifiedResource Rename(string url, string newName)
        {
            var resource = Get(url);
            var action = resource.GetAction<IRenameAction>();

            if (action != null)
            {
                var newUrl = action.Rename(url, newName);
                resource = Get(newUrl);
            }

            return resource;
        }

        public IEnumerable<UnifiedResource> GetChildren(string url)
        {
            var resource = Get(url);
            var action = resource.GetAction<IScanAction>();
            var ret = action?.GetChildrenResources(url).ToList();
            
            return ret;
        }

        public Stream Download(string url)
        {
            var resource = Get(url);
            var action = resource.GetAction<IDownloadAction>();
            var ret = action?.Download(url);

            return ret;
        }

        public UnifiedResource Create(string url, string name)
        {
            var resource = Get(url);
            var action = resource.GetAction<ICreateAction>();

            return action?.Create(url, name);
        }

        public async Task<UnifiedResource> UploadAsync(string url, string name, byte[] data)
        {
            var resource = Get(url);
            var action = resource.GetAction<IUploadAction>();

            return await action?.UploadAsync(url, name, data);
        }

        public IMetadata GetMetadata(string url)
        {
            var resource = Get(url);
            var action = resource.GetAction<IMetadataAction>();

            return action?.GetMetadata(url);
        }
    }
}