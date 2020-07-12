using NWN.Amia.Main.Core;
using NWN.Amia.Main.Core.Types;
using Xunit;

namespace NWN.Amia.test
{
    public class ScriptRunnerTest
    {
        [Fact]
        public void ScriptRunnerMockScriptReturnsZero()
        {
            var scriptContext = new ScriptContext {OwnerObject = 9, ScriptName = "z_real_script_name"};
            var runner = new ScriptHandler(scriptContext);
            Assert.Equal(0,
                runner.HandleContext());
        }

        [Fact]
        public void ScriptRunnerWillNotResolveUnmanaged()
        {
            var unmanagedContext = new ScriptContext() {OwnerObject = 0, ScriptName = "fake_name"};
            var runner = new ScriptHandler(unmanagedContext);
            Assert.Equal(-1, runner.HandleContext());
        }
    }
}