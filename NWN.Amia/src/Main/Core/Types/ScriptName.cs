using System;

namespace NWN.Amia.Main.Core.Types
{
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