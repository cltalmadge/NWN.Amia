using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Main.Managed
{
    [ScriptName("z_real_script_name")]
    public class MockScript : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            return 0;
        }
    }
}