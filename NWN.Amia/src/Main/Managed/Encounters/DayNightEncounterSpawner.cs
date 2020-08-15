﻿using System;
using System.Collections.Generic;
using System.Linq;
using NWN.Amia.Main.Managed.Encounters.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Encounters
{
    public class DayNightEncounterSpawner : IEncounterSpawner
    {
        private static IntPtr _waypointLocation;
        private static uint _objectWithVariables;
        private static readonly string[] VarPrefixes = {"day_spawn", "night_spawn"};
        private readonly uint _player;
        private readonly uint _trigger;


        public DayNightEncounterSpawner(uint trigger, uint objectWithVariables)
        {
            _trigger = trigger;
            _objectWithVariables = objectWithVariables;
        }

        public DayNightEncounterSpawner(uint trigger, uint objectWithVariables, uint player)
        {
            _trigger = trigger;
            _objectWithVariables = objectWithVariables;
            _player = player;
        }

        public bool DoubleSpawn { get; set; }

        public void SpawnEncounters()
        {
            _waypointLocation = GetLocationForSpawn();

            var isNightTime = NWScript.GetTimeHour() < 6 && NWScript.GetTimeHour() >= 18;

            var spawnsVary = NWScript.GetLocalInt(_trigger, "spawns_vary") == 1;

            var spawnsToChoose = isNightTime && spawnsVary ? VarPrefixes[1] : VarPrefixes[0];

            var dayCreatureResRefs = GetResRefsForPrefix(spawnsToChoose) as string[] ??
                                     GetResRefsForPrefix(spawnsToChoose).ToArray();

            var numToSpawn = NWScript.d3() + 4;
            var maxSpawns = DoubleSpawn ? numToSpawn * 2 : numToSpawn;
            SpawnCreaturesFromResRefs(maxSpawns, dayCreatureResRefs);
        }

        private IntPtr GetLocationForSpawn()
        {
            // NWScript.Vector
            return NWScript.GetLocation(NWScript.GetNearestObjectByTag("ds_spwn", _trigger));
        }

        private static void SpawnCreaturesFromResRefs(int maxSpawns, IReadOnlyList<string> resRefs)
        {
            if (!resRefs.Any()) return;

            for (var i = 0; i < maxSpawns; i++)
            {
                var randomCreature = new Random().Next(0, resRefs.Count);
                SpawnEncounterAtWaypoint(resRefs[randomCreature]);
            }
        }

        private static void SpawnEncounterAtWaypoint(string resRef)
        {
            NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE, resRef, _waypointLocation);
        }

        private static IEnumerable<string> GetResRefsForPrefix(string prefix)
        {
            var resRefs = new List<string>();

            var numberOfLocalVars = ObjectPlugin.GetLocalVariableCount(_objectWithVariables);

            for (var i = 0; i < numberOfLocalVars; i++)
            {
                var variableName = ObjectPlugin.GetLocalVariable(_objectWithVariables, i).key;
                if (variableName.Contains(prefix))
                    resRefs.Add(NWScript.GetLocalString(_objectWithVariables, variableName));
            }

            return resRefs;
        }
    }
}