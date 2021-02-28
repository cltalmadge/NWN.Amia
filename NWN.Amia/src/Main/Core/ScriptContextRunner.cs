using System;
using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Main.Core
{
    public class ScriptContextRunner : IScriptContextRunner
    {
        private readonly ScriptContext _currentScript;

        internal ScriptContextRunner(ScriptContext currentScript)
        {
            _currentScript = currentScript;
        }

        public int RunScript()
        {
            var scriptResult = 1;
            try
            {
                scriptResult = GetManagedScriptFromContext().Run(_currentScript.CallingObject);
            }
            catch (Exception m)
            {
                Console.WriteLine($"FAILED TO PARSE SCRIPT {_currentScript.ScriptName}. Reason: {m.Message}");
            }

            return scriptResult;
        }

        private IRunnableScript GetManagedScriptFromContext()
        {
            return ManagedScripts.GetScriptFromName(_currentScript.ScriptName);
        }
    }
}