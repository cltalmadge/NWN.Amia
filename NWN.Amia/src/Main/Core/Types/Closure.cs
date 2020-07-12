using NWN.Core;

namespace NWN.Amia.Main.Core.Types
{
    public struct Closure
    {
        public uint OwnerObject;
        public ActionDelegate Run;
    }
}