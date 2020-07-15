using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Main.Core
{
    public static class ScriptDictionary
    {
        public static Dictionary<string, Type> StoredScripts { get; } = new Dictionary<string, Type>();

        public static bool Initialized { get; set; }

        public static void PerformInitialSetup()
        {
            Console.WriteLine("Performing initial setup for script dictionary.");
            
            foreach (var type in GetTypesInAssembly(Assembly.GetExecutingAssembly()))
            {
                var scriptName = (ScriptName) Attribute.GetCustomAttribute(type, typeof(ScriptName));

                if (scriptName?.Name != null)
                {
                    StoredScripts.TryAdd(scriptName.Name, type);
                }
            }

            Initialized = true;
        }

        public static IRunnableScript GetScriptFromName(string scriptName)
        {
            if (!Initialized)
            {
                PerformInitialSetup();
            }
            
            IRunnableScript scriptToRun;

            try
            {
                scriptToRun =
                    (IRunnableScript) Activator.CreateInstance(
                        StoredScripts[scriptName]);
            }
            catch (Exception e)
            {
                scriptToRun = new UnhandledScript();
            }

            return scriptToRun;
        }
        

        private static IEnumerable<Type> GetTypesInAssembly(Assembly assembly)
        {
            return assembly.GetTypes().ToArray();
        }
    }
}