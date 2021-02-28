using NWN.Amia.Main.Core;
using NWN.Amia.Main.Core.Types;
using Xunit;

namespace NWN.Amia.Test
{
    public class ScriptRunnerTest
    {
        private readonly ScriptContext _managedContext = new ScriptContext
            {CallingObject = 9, ScriptName = "z_real_script_name"};

        private readonly ScriptContext _managedContextNested = new ScriptContext
            {CallingObject = 9, ScriptName = "nested_script"};

        private readonly ScriptContext _unmanagedContext = new ScriptContext
            {CallingObject = 0, ScriptName = "fake_name"};

        private IScriptContextRunner _runner;

        [Fact]
        public void ScriptRunnerCanFindScriptsInSubdirectories()
        {
            _runner = new ScriptContextRunner(_managedContextNested);
            Assert.Equal(0, _runner.RunScript());
        }

        [Fact]
        public void ScriptRunnerMockScriptReturnsZero()
        {
            _runner = new ScriptContextRunner(_managedContext);
            Assert.Equal(0, _runner.RunScript());
        }

        [Fact]
        public void ScriptRunnerReturnsFailureOnNullData()
        {
            _runner = new ScriptContextRunner(new ScriptContext {ScriptName = null, CallingObject = 0});
            Assert.Equal(-1, _runner.RunScript());
        }

        [Fact]
        public void ScriptRunnerWillNotResolveUnmanaged()
        {
            _runner = new ScriptContextRunner(_unmanagedContext);
            Assert.Equal(-1, _runner.RunScript());
        }
    }
}