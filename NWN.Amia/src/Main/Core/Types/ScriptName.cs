namespace NWN.Amia.Main.Core.Types
{
    public class ScriptName : System.Attribute
    {
        public string Name { get; }

        public ScriptName(string name)
        {
            Name = name;
        }
    }
}