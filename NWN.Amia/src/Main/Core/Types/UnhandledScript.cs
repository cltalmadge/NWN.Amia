namespace NWN.Amia.Main.Core.Types
{
    public class InvalidScript : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            return -1;
        }
    }
}