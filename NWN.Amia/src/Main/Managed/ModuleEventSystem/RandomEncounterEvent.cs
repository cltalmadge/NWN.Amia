using NWN.Amia.Main.Managed.ModuleEventSystem.Types;

namespace NWN.Amia.Main.Managed.ModuleEventSystem
{
    public class RandomEncounterEvent : IModuleEvent
    {
        private IRandomEncounter _encounter;

        public RandomEncounterEvent(IRandomEncounter encounter)
        {
            _encounter = encounter;
        }

        public void InvokeEvent()
        {
            
        }
    }
}