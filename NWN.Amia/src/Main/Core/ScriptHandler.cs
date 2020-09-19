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
            var scriptResult = 1;
            try
            {
                scriptResult = GetScriptFromContext().Run(_currentScript.OwnerObject);
            }
            catch (Exception m)
            {
                Console.WriteLine($"FAILED TO PARSE SCRIPT {_currentScript.ScriptName}. Reason: {m.Message}");
            }

            return scriptResult;
        }

        private IRunnableScript GetScriptFromContext()
        {
            return ManagedScripts.GetScriptFromName(_currentScript.ScriptName);
        }
    }
}