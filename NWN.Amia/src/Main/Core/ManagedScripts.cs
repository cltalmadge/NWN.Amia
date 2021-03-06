﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Main.Core
{
    public static class ManagedScripts
    {
        private static ConcurrentDictionary<string, Type> StoredScripts { get; } =
            new();

        private static bool Initialized { get; set; }

        private static void PerformInitialSetup()
        {
            Console.WriteLine("Performing initial setup. Reflectively grabbing all types with property [ScriptName].");

            foreach (var type in GetTypesInAssembly(Assembly.GetExecutingAssembly()))
            {
                var scriptName = (ScriptName) Attribute.GetCustomAttribute(type, typeof(ScriptName));

                if (scriptName?.Name == null) continue;

                StoredScripts.TryAdd(scriptName.Name, type);
                Console.WriteLine($"Cached script {scriptName.Name}.");
            }

            Initialized = true;
        }
        
        
        public static IRunnableScript GetScriptFromName(string scriptName)
        {
            if (!Initialized) PerformInitialSetup();

            IRunnableScript scriptToRun;

            try
            {
                scriptToRun =
                    (IRunnableScript) Activator.CreateInstance(
                        StoredScripts[scriptName]);
            }
            catch (Exception)
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