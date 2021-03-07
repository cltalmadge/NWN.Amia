namespace NWN.Amia.Main.Managed.ModuleEventSystem.Types
{
    public interface IModuleEventSystem
    {
        void InvokeEvent(IModuleEvent @event);
    }
}