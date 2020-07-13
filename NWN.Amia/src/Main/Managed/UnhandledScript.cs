using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Main.Managed
{
    public class InvalidScript : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            return -1;
        }
    }
}