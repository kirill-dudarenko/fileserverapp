using Common;
using Common.Enums;
using Common.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace fileserverapp.Models
{
    public class ResourceDto
    {
        public string key { get; set; }
        public string parent { get; set; }
        public string title { get; set; }
        public IEnumerable<string> Actions { get; set; }
        public ResourceType Type { get; set; }

        public bool folder { get; }
        public bool lazy { get; } 

        public readonly object[] children;

        public ResourceDto()
        {}

        public ResourceDto(UnifiedResource resource)
        {
            key = resource.Id.ToBase64String();
            parent = resource.Location.ToBase64String();
            Type = resource.Type;
            title = resource.Name;
            Actions = resource.Actions
                              .Select(x => x.ActionType.ToString())
                              .ToArray();
            folder = Type == ResourceType.Root || Type == ResourceType.Drive || Type == ResourceType.Folder;
            lazy = true;
            children = folder ? null : new object[0];
        }
    }
}