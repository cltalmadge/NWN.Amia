using System;
using System.Linq;
using NWN.Amia.Main.Managed.ModuleEventSystem.Types;
using NWN.Amia.Main.Managed.Objects;

namespace NWN.Amia.Main.Managed.ModuleEventSystem
{
    public class RandomEncounterSystem : IModuleEventSystem
    {
        private Area _area;

        public void InvokeEvent(IModuleEvent moduleEvent)
        {
            moduleEvent.InvokeEvent();
        }

        // TODO: Change "waypoint_tag" so that it actually reflects what Waypoints are going to be. 
        public bool HasEventNodes() => _area.Waypoints.Any(w => w.Tag.Equals("waypoint_tag"));
    }
}