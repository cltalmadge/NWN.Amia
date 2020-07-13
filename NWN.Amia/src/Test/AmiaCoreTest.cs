using NWN.Amia.Main.Core;
using Xunit;

namespace NWN.Amia.Test
{
    public class AmiaCoreTest
    {
        private readonly AmiaCore _amiaCore;

        public AmiaCoreTest()
        {
            _amiaCore = new AmiaCore();
        }

        [Fact]
        public void OnRunScriptHandlesScripts()
        {
            Assert.Equal(0, _amiaCore.OnRunScript("z_real_script_name", 0));
        }

        [Fact]
        public void OnRunScriptCannotHandleNonExistent()
        {
            Assert.Equal(-1, _amiaCore.OnRunScript("", 0));
        }

        [Fact]
        public void ObjectSelfIsSetToOidSelf()
        {
            _amiaCore.OnRunScript("", 0);
            Assert.Equal((uint)0, _amiaCore.ObjectSelf);
        }

        [Fact]
        public void OnClosureResetsObjectSelfToOldValue()
        {
            _amiaCore.OnRunScript("", 1);
            _amiaCore.OnClosure(0, 0);
            
            Assert.Equal(_amiaCore.ObjectSelf, (uint)1);
        }
    }
}