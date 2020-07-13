using NWN.Amia.Main.Core.Types;

namespace NWN.Amia.Test.Mock.Nested
{
    [ScriptName("nested_script")]
    public class NestedScript : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            return 0;
        }
    }
}