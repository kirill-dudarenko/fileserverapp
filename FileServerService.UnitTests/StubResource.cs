using Common;
using Common.Interfaces;
using Common.Interfaces.Actions;
using System;
using System.Collections.Generic;

namespace FileServerService.UnitTests
{
    public class StubResource : UnifiedResource
    {
        public StubResource(IResourceFactory mockFactory) :base(null,null, mockFactory)
        {

        }

        public void SetAction(IAction mockAction)
        {
            Actions.Add(mockAction);
        }

        protected override List<IAction> SetActions()
        {
            return new List<IAction>();
        }

        public override T GetAction<T>()
        {
            return (T) Actions[0];
        }
    }
}
