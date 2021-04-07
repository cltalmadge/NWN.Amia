using System;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

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
                NWScript.WriteTimestampedLogEntry($"FAILED TO PARSE SCRIPT {_currentScript.ScriptName}. Reason: {m.Message} \n({m.InnerException?.Message}, {m.InnerException?.StackTrace}\n => {m.StackTrace})");
            }

            return scriptResult;
        }

        private IRunnableScript GetManagedScriptFromContext()
        {
            return ManagedScripts.GetScriptFromName(_currentScript.ScriptName);
        }
    }
}