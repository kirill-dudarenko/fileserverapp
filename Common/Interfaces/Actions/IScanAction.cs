using System.Collections.Generic;

namespace Common.Interfaces.Actions
{
    public interface IScanAction : IAction
    {
        IEnumerable<UnifiedResource> GetChildrenResources(string url);
    }
}
