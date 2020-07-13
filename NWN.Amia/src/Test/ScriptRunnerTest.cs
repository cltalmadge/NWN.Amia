using NWN.Amia.Main.Core;
using NWN.Amia.Main.Core.Types;
using Xunit;

namespace NWN.Amia.Test
{
    public class ScriptRunnerTest
    {
        private readonly ScriptContext _managedContext = new ScriptContext
            {OwnerObject = 9, ScriptName = "z_real_script_name"};

        private readonly ScriptContext _managedContextNested = new ScriptContext
            {OwnerObject = 9, ScriptName = "nested_script"};

        private readonly ScriptContext _unmanagedContext = new ScriptContext
            {OwnerObject = 0, ScriptName = "fake_name"};

        private ScriptHandler _runner;

        [Fact]
        public void ScriptRunnerMockScriptReturnsZero()
        {
            _runner = new ScriptHandler(_managedContext);
            Assert.Equal(0,
                _runner.HandleContext());
        }

        [Fact]
        public void ScriptRunnerWillNotResolveUnmanaged()
        {
            _runner = new ScriptHandler(_unmanagedContext);
            Assert.Equal(-1, _runner.HandleContext());
        }

        [Fact]
        public void ScriptRunnerCanFindScriptsInSubdirectories()
        {
            _runner = new ScriptHandler(_managedContextNested);
            Assert.Equal(0, _runner.HandleContext());
        }
    }
}