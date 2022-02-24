
using Common.Enums;
using Common.Interfaces;
using Common.Interfaces.Actions;
using Common.Interfaces.MetaData;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common
{
    public abstract class UnifiedResource
    {
        public string Location { get; private set; }
        public string Name { get; set; }
        public ResourceType Type { get; set; }
        public readonly List<IAction> Actions;

        protected abstract List<IAction> SetActions();
        protected IResourceFactory factory;

        public UnifiedResource(string url, string name, IResourceFactory resourceFactory)
        {
            factory = resourceFactory;
            Location = url;
            Name = name;
            Actions = SetActions();
        }

        public virtual T GetAction<T>()
        {
            var action = Actions.FirstOrDefault(x => x is T);
            return (T)action;
        }
                
        public virtual string Id
        {
            get { return Path.Combine(Location ?? string.Empty, Name); }
        }
    }
}
