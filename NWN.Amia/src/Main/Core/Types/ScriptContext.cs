namespace NWN.Amia.Main.Core.Types
{
    // Script context stores the name of the script and the object id of the entity calling the script.
    public struct ScriptContext
    {
        public uint CallingObject;
        public string ScriptName;
    }
}