using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Core
{
    [UsedImplicitly]
    public class AmiaCore : IGameManager
    {
        public static AmiaCore Instance { get; } = new AmiaCore();
        public uint ObjectSelf { get; private set; } = NWScript.OBJECT_INVALID;
        private ulong NextEventId { get; set; }
        
        private readonly Stack<ScriptContext> _scriptContexts = new Stack<ScriptContext>();
        private readonly Dictionary<ulong, Closure> _closures = new Dictionary<ulong, Closure>();

        public static int Bootstrap(IntPtr ptr, int nativeHandlesLength) {
            // Call internal bootstrap function
            return Internal.Init(ptr, nativeHandlesLength, Instance);
        }

        public void OnMainLoop(ulong frame)
        {
            // Don't do anything.
        }

        public int OnRunScript(string script, uint oidSelf)
        {
            ObjectSelf = oidSelf;
            
            var scriptBeingCalled = new ScriptContext {OwnerObject = oidSelf, ScriptName = script};
            IContextHandler contextHandler = new ScriptHandler(scriptBeingCalled);

            return contextHandler.HandleContext();
        }

        public void OnClosure(ulong eid, uint oidSelf)
        {
            var old = ObjectSelf;
            ObjectSelf = oidSelf;


            RunClosure(eid);

            ObjectSelf = old;
        }

        private void RunClosure(ulong eid)
        {
            try
            {
                _closures[eid].Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            _closures.Remove(eid);
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