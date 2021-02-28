namespace NWN.Amia.Main.Core.Types
{
    // Any class that is meant to run scripts should implement this method. 
    public interface IScriptContextRunner
    {
        int RunScript();
    }
}