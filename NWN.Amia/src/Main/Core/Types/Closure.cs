using System;
using NWN.Core;

namespace NWN.Amia.Main.Core.Types
{
    public struct Closure
    {
        public uint OwnerObject;
        public Action Run;
    }
}