using System;

namespace NWN.Amia.Main.Core.Types
{
    /// <summary>
    /// A script name attribute to mark a class as a script with their in game NWN script name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ScriptName : Attribute
    {
        public ScriptName(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}