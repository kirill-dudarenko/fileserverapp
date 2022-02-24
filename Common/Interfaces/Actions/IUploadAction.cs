using System.Threading.Tasks;

namespace Common.Interfaces.Actions
{
    public interface IUploadAction : IAction
    {
        Task<UnifiedResource> UploadAsync(string url, string name, byte[] data);
    }
}
