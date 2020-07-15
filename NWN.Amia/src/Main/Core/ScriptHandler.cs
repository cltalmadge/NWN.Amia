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
            Console.WriteLine($"Resolving class associated with script: {_currentScript.ScriptName}.");

            return GetScriptFromContext().Run(_currentScript.OwnerObject);
        }

        private IRunnableScript GetScriptFromContext()
        {
            return ScriptDictionary.GetScriptFromName(_currentScript.ScriptName);
        }
    }
}