using System;
using NWN.Amia.Main.Managed.Objects.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Objects
{
    public class Waypoint : GameObject
    {
        public IntPtr Location { get; set; }

        public Waypoint(uint objectId) : base(objectId)
        {
        }
    }
}