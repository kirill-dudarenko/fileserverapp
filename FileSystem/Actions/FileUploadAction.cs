using Common;
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using System.IO;
using System.Threading.Tasks;

namespace FileSystem.Actions
{
    public class FileUploadAction : IUploadAction
    {
        protected readonly IResourceFactory factory;
        public ActionType ActionType => ActionType.Upload;

        public FileUploadAction(IResourceFactory resourceFactory)
        {
            factory = resourceFactory;
        }
        public async Task<UnifiedResource> UploadAsync(string url, string name, byte[] data)
        {
            var file = Path.Combine(url, name);
            //File.WriteAllBytes(file, data);

            using (var fs = new FileStream(Path.Combine(url, name), FileMode.Create))
            {
                await fs.WriteAsync(data, 0, data.Length);

                return factory.Create(file);
            }
        }
    }
}
