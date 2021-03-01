namespace NWN.Amia.Main.Core.Types
{
    // Every script must implement this interface and have a [ScriptName] attribute.
    public interface IRunnableScript
    {
        int Run(uint nwnObjectId);
    }
}