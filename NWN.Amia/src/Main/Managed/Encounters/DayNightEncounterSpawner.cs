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
        private static uint _area;
        private static readonly string[] VarPrefixes = {"day_spawn", "night_spawn"};
        private readonly uint _trigger;
        private const string DsSpwn = "ds_spwn";

        public DayNightEncounterSpawner(uint trigger, uint area)
        {
            _trigger = trigger;
            _area = area;
        }

        public bool IsDoubleSpawn { get; set; }

        public void SpawnEncounters()
        {
            SetSpawnPointToRandomWaypointWithinTrigger();

            NWScript.WriteTimestampedLogEntry($"Sourcing spawns in area: {NWScript.GetName(_area)}");

            var isNightTime = NWScript.GetTimeHour() < 6 || NWScript.GetTimeHour() >= 18;
            NWScript.WriteTimestampedLogEntry($"Time is {NWScript.GetTimeHour()} and isNightTime == {isNightTime}.");

            var spawnsVary = NWScript.GetLocalInt(_area, "spawns_vary") == 1;
            NWScript.WriteTimestampedLogEntry($"Spawns vary is {spawnsVary}.");

            var spawnsToChoose = isNightTime && spawnsVary ? VarPrefixes[1] : VarPrefixes[0];
            NWScript.WriteTimestampedLogEntry($"Choosing spawns from prefix {spawnsToChoose}.");

            var dayCreatureResRefs = GetResRefsForPrefix(spawnsToChoose) as string[] ??
                                     GetResRefsForPrefix(spawnsToChoose).ToArray();

            var numToSpawn = NWScript.d4() + 2;
            var maxSpawns = IsDoubleSpawn ? numToSpawn * 2 : numToSpawn;
            SpawnCreaturesFromResRefs(maxSpawns, dayCreatureResRefs);
        }

        private void SetSpawnPointToRandomWaypointWithinTrigger()
        {
            // var waypoint = NWScript.GetNearestObjectByTag("ds_spwn", _trigger);
            // _spawnLocation = NWScript.GetLocation(waypoint);
            var waypoint = NWScript.GetFirstInPersistentObject(_trigger, NWScript.OBJECT_TYPE_WAYPOINT);
            var spawnPoints = GetNumberOfSpawnPointsInTrigger() as uint[] ?? GetNumberOfSpawnPointsInTrigger().ToArray();
            var randomSpawnPosition = new Random().Next(0, spawnPoints.Length);
            
            _spawnLocation = NWScript.GetLocation(spawnPoints[randomSpawnPosition]);
        }

        private IEnumerable<uint> GetNumberOfSpawnPointsInTrigger()
        {
            var waypoint = NWScript.GetFirstInPersistentObject(_trigger, NWScript.OBJECT_TYPE_WAYPOINT);
            var spawnPointsInTrigger = new List<uint>();
            
            while (waypoint != NWScript.OBJECT_INVALID)
            {
                waypoint = NWScript.GetNextInPersistentObject(_trigger, NWScript.OBJECT_TYPE_WAYPOINT);
                if(NWScript.GetTag(waypoint).Equals(DsSpwn)) spawnPointsInTrigger.Add(waypoint);
            }

            return spawnPointsInTrigger;
        }
        
        private static IEnumerable<string> GetResRefsForPrefix(string prefix)
        {
            List<string> resRefs = new();

            int numberOfLocalVars = ObjectPlugin.GetLocalVariableCount(_area);

            if (numberOfLocalVars == 0)
            {
                NWScript.WriteTimestampedLogEntry($"ERROR: No spawns for {NWScript.GetName(_area)}!!! Aborted!");
                return new List<string>();
            }

            for (int i = 0; i < numberOfLocalVars; i++)
            {
                var variableName = ObjectPlugin.GetLocalVariable(_area, i).key;
                if (variableName.Contains(prefix))
                    resRefs.Add(NWScript.GetLocalString(_area, variableName));
            }

            return resRefs;
        }

        private static void SpawnCreaturesFromResRefs(int maxSpawns, IReadOnlyList<string> resRefs)
        {
            if (!resRefs.Any())
            {
                NWScript.WriteTimestampedLogEntry(
                    $"Attempted to spawn creatures in {NWScript.GetName(NWScript.GetAreaFromLocation(_spawnLocation))}, but there were no creatures to spawn.");
                return;
            }

            for (int i = 0; i < maxSpawns; i++)
            {
                int randomCreature = new Random().Next(0, resRefs.Count);
                SpawnEncounterAtWaypoint(resRefs[randomCreature]);
            }
        }

        private static void SpawnEncounterAtWaypoint(string resRef)
        {
            if (resRef.Equals(""))
            {
                NWScript.WriteTimestampedLogEntry("Found empty resref! Aborting!");
                return;
            }

            var creature = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE, resRef, _spawnLocation);

            NWScript.DestroyObject(creature, 600.0f);

            NWScript.ChangeToStandardFaction(creature, NWScript.STANDARD_FACTION_HOSTILE);

            if (creature == NWScript.OBJECT_INVALID)
            {
                NWScript.WriteTimestampedLogEntry(
                    $"Spawn wasn't valid: {resRef} not valid and creature returned OBJECT_INVALID");
            }
        }
    }
}