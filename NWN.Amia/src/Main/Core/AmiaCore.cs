﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Core
{
    [UsedImplicitly]
    public class AmiaCore : IGameManager
    {
        private readonly Dictionary<ulong, Closure> _closures = new Dictionary<ulong, Closure>();

        private readonly Stack<ScriptContext> _scriptContexts = new Stack<ScriptContext>();
        public static AmiaCore Instance { get; } = new AmiaCore();
        private ulong NextEventId { get; set; }
        public uint ObjectSelf { get; private set; } = NWScript.OBJECT_INVALID;

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

        public void OnSignal(string signal)
        {
            
        }

        public void ClosureAssignCommand(uint obj, ActionDelegate func)
        {
            if (VM.ClosureAssignCommand(obj, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }

        public void ClosureDelayCommand(uint obj, float duration, ActionDelegate func)
        {
            if (VM.ClosureDelayCommand(obj, duration, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }

        public void ClosureActionDoCommand(uint obj, ActionDelegate func)
        {
            if (VM.ClosureActionDoCommand(obj, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }

        public static int Bootstrap(IntPtr ptr, int nativeHandlesLength)
        {
            // Call internal bootstrap function
            return NWNCore.Init(ptr, nativeHandlesLength, Instance);
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
    }
}