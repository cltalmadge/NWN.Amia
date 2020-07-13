using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Test.Mock
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