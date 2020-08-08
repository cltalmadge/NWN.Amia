using System;
using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Main.Core
{
    public class ScriptHandler : IContextHandler
    {
        private readonly ScriptContext _currentScript;

        internal ScriptHandler(ScriptContext currentScript)
        {
            _currentScript = currentScript;
        }

        public int HandleContext()
        {
            return GetScriptFromContext().Run(_currentScript.OwnerObject);
        }

        private IRunnableScript GetScriptFromContext()
        {
            return ManagedScripts.GetScriptFromName(_currentScript.ScriptName);
        }
    }
}