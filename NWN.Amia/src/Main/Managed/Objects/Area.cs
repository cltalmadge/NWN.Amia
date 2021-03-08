using System.Collections;
using System.Collections.Generic;
using NWN.Amia.Main.Managed.Objects.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Objects
{
    public class Area : GameObject
    {
        public IEnumerable<Waypoint> Waypoints { get; set; }
        // public IEnumerable<Trigger> Triggers { get; set; }
        public IEnumerable<ICreature> AllCreatures { get; set; }
        public IDictionary<string, List<ICreature>> Creatures { get; set; }
        public IEnumerable<Player> Players { get; set; }

        public Area(uint objectId) : base(objectId)
        {
        }
    }
}