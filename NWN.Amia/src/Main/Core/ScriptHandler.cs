using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed;

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
            IRunnableScript scriptToRun = new InvalidScript();

            foreach (var type in GetTypesInNamespace(Assembly.GetExecutingAssembly(), "NWN.Amia.Main.Managed"))
            {
                ScriptName scriptName = (ScriptName) Attribute.GetCustomAttribute(type, typeof(ScriptName));
                
                Console.Write(type.Assembly.FullName);
                if (null != scriptName && scriptName.Name == _currentScript.ScriptName)
                {
                    scriptToRun = (IRunnableScript) Activator.CreateInstance(type);
                }
            }

            return scriptToRun;
        }

        private static IEnumerable<Type> GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
                assembly.GetTypes()
                    .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                    .ToArray();
        }
    }
}