using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            IRunnableScript scriptToRun = new InvalidScript();

            foreach (var type in GetTypesInAssembly(Assembly.GetExecutingAssembly()))
            {
                ScriptName scriptName = (ScriptName) Attribute.GetCustomAttribute(type, typeof(ScriptName));

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
                    .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                    .ToArray();
        }

        private static IEnumerable<Type> GetTypesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes().ToArray();
        }
    }
}