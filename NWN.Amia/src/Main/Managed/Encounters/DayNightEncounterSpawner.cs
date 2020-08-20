using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NWN.Amia.Main.Managed.Encounters.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Encounters
{
    public class DayNightEncounterSpawner : IEncounterSpawner
    {
        private static IntPtr _spawnLocation;
        private static uint _objectWithVariables;
        private static readonly string[] VarPrefixes = {"day_spawn", "night_spawn"};
        private readonly uint _player;
        private readonly uint _trigger;

        public DayNightEncounterSpawner(uint trigger, uint objectWithVariables, uint player)
        {
            _trigger = trigger;
            _objectWithVariables = objectWithVariables;
            _player = player;
        }

        public bool DoubleSpawn { get; set; }

        public void SpawnEncounters()
        {
            SetSpawnPointToNearestWaypoint();

            var isNightTime = NWScript.GetTimeHour() < 6 || NWScript.GetTimeHour() >= 18;
            NWScript.WriteTimestampedLogEntry($"Time is {NWScript.GetTimeHour()} and isNightTime == {isNightTime}.");
            
            var spawnsVary = NWScript.GetLocalInt(_objectWithVariables, "spawns_vary") == 1;
            NWScript.WriteTimestampedLogEntry($"Spawns vary is {spawnsVary}.");

            var spawnsToChoose = isNightTime && spawnsVary ? VarPrefixes[1] : VarPrefixes[0];
            NWScript.WriteTimestampedLogEntry($"Choosing spawns from prefix {spawnsToChoose}.");

            var dayCreatureResRefs = GetResRefsForPrefix(spawnsToChoose) as string[] ??
                                     GetResRefsForPrefix(spawnsToChoose).ToArray();

            var numToSpawn = NWScript.d4() + 2;
            var maxSpawns = DoubleSpawn ? numToSpawn * 2 : numToSpawn;
            SpawnCreaturesFromResRefs(maxSpawns, dayCreatureResRefs);
        }

        private void SetSpawnPointToNearestWaypoint()
        {
            var waypoint = NWScript.GetNearestObjectByTag("ds_spwn", _trigger);
            _spawnLocation = NWScript.GetLocation(waypoint);
        }

        private static void SpawnCreaturesFromResRefs(int maxSpawns, IReadOnlyList<string> resRefs)
        {
            if (!resRefs.Any())
            {
                LogEmptySpawns();
                return;
            }

            NWScript.ApplyEffectAtLocation(NWScript.DURATION_TYPE_INSTANT, NWScript.EffectVisualEffect(247),
                _spawnLocation);

            for (var i = 0; i < maxSpawns; i++)
            {
                var randomCreature = new Random().Next(0, resRefs.Count);
                SpawnEncounterAtWaypoint(resRefs[randomCreature]);
            }
        }

        private static void LogEmptySpawns() =>
            NWScript.WriteTimestampedLogEntry(
                $"Attempted to spawn creatures in {NWScript.GetName(NWScript.GetAreaFromLocation(_spawnLocation))}, but there were no creatures to spawn.");

        private static void SpawnEncounterAtWaypoint(string resRef)
        {
            var creature = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE, resRef, _spawnLocation);
            NWScript.DestroyObject(creature, 600.0f);
            NWScript.ChangeToStandardFaction(creature, NWScript.STANDARD_FACTION_HOSTILE);
            if (creature == NWScript.OBJECT_INVALID)
            {
                NWScript.WriteTimestampedLogEntry(
                    $"Spawn wasn't valid: {resRef} not valid and creature returned OBJECT_INVALID");
            }
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