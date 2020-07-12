using System.Collections.Generic;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Core
{
    [UsedImplicitly]
    public class AmiaCore : IGameManager
    {
        [UsedImplicitly] public uint ObjectSelf { get; }

        private ulong NextEventId { get; set; }

        private readonly Stack<ScriptContext> _scriptContexts = new Stack<ScriptContext>();
        private readonly Dictionary<ulong, Closure> _closures = new Dictionary<ulong, Closure>();


        public void OnMainLoop(ulong frame)
        {
            throw new System.NotImplementedException();
        }

        public int OnRunScript(string script, uint oidSelf)
        {
            var scriptBeingCalled = new ScriptContext {OwnerObject = oidSelf, ScriptName = script};

            IContextHandler contextHandler = new ScriptHandler(scriptBeingCalled);
            return contextHandler.HandleContext();
        }

        public void OnClosure(ulong eid, uint oidSelf)
        {
            throw new System.NotImplementedException();
        }

        public void ClosureAssignCommand(uint obj, ActionDelegate func)
        {
            if (Internal.NativeFunctions.ClosureAssignCommand(obj, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }

        public void ClosureDelayCommand(uint obj, float duration, ActionDelegate func)
        {
            if (Internal.NativeFunctions.ClosureDelayCommand(obj, duration, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }

        public void ClosureActionDoCommand(uint obj, ActionDelegate func)
        {
            if (Internal.NativeFunctions.ClosureActionDoCommand(obj, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }
    }
}