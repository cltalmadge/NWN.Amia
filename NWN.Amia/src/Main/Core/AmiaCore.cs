using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Core
{
    [UsedImplicitly]
    public class AmiaCore : ICoreEventHandler, ICoreFunctionHandler
    {
        private readonly Dictionary<ulong, Closure> _closures = new();

        private static AmiaCore Instance { get; } = new();
        private ulong NextEventId { get; set; }
        public uint ObjectSelf { get; private set; } = NWScript.OBJECT_INVALID;

        // Don't do anything here if at all possible. Running procedures here can be very expensive since this method is
        // run every iteration of the main loop for NWN.
        public void OnMainLoop(ulong frame)
        {
        }


        /**
         * <summary>
         *     This method is called every time the OnRunScript event fires off in NWN. Interrupts NWScript's execution of any
         *     managed scripts and hands off processing of the managed script to a
         *     <see cref="IScriptContextRunner" />
         * </summary>
         * <returns>
         *     An integer representation of an exit code that will be processed by NWScript. Just like in C, 1 represents
         *     failure, 0 is success. -1 passes execution of the script back to NWScript, which will then call the original script
         *     as normal.
         * </returns>
         */
        public int OnRunScript(string script, uint oidSelf)
        {
            ObjectSelf = oidSelf;

            ScriptContext scriptBeingCalled = new() {CallingObject = oidSelf, ScriptName = script};
            IScriptContextRunner scriptContextRunner = new ScriptContextRunner(scriptBeingCalled);

            int onRunScript = scriptContextRunner.RunScript();
    
            return onRunScript;
        }

        /**
         * <summary>
         *     Closure functions deal with delayed commands and other engine actions that happen at a later time. They are called
         *     by the engine.
         *     It is not recommended to change or alter these unless you know what you are doing and want a specific result.
         * </summary>
         */
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

  
        public void ClosureAssignCommand(uint obj, Action func)
        {
            if (VM.ClosureAssignCommand(obj, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }

        public void ClosureDelayCommand(uint obj, float duration, Action func)
        {
            if (VM.ClosureDelayCommand(obj, duration, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
        }

        public void ClosureActionDoCommand(uint obj, Action func)
        {
            if (VM.ClosureActionDoCommand(obj, NextEventId) != 0)
                _closures.Add(NextEventId++, new Closure {OwnerObject = obj, Run = func});
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

        /**
         * <summary>
         *     This is where the magic happens. The NWNX C# plugin runs this bootstrap function when it loads the library,
         *     which is configured in the server's environment variable, and hooks NWN.Amia into the game.
         * </summary>
         */
        [UsedImplicitly]
        public static int Bootstrap(IntPtr ptr, int nativeHandlesLength)
        {
            return NWNCore.Init(ptr, nativeHandlesLength, Instance);
        }
    }
}