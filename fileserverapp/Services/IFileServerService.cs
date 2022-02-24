using Common;
using Common.Interfaces.MetaData;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace fileserverapp.Services
{
    public interface IFileServerService
    {
        UnifiedResource Get(string url);

        IEnumerable<UnifiedResource> GetChildren(string url);

        void Delete(string url);

        UnifiedResource Copy(string url);

        UnifiedResource Rename(string url, string newName);

        Stream Download(string url);

        UnifiedResource Create(string url, string name);

        Task<UnifiedResource> UploadAsync(string url, string name, byte[] data);

        IMetadata GetMetadata(string url);
    }
}
