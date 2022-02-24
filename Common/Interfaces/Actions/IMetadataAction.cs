using Common.Interfaces.MetaData;

namespace Common.Interfaces.Actions
{
    public interface IMetadataAction : IAction
    {
        IMetadata GetMetadata(string url);
    }
}
