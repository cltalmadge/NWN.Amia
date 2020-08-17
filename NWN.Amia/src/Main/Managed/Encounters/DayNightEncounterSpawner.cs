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
            _spawnLocation = GetLocationForSpawn();

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
            var playerLocation = NWScript.GetPosition(_player);
            return NWScript.Location(NWScript.GetArea(_player),
                GetPositionInFront(playerLocation, 15, NWScript.GetFacing(_player)), NWScript.GetFacing(_player));
        }

        private static Vector3 GetPositionInFront(Vector3 original, float distance, float angle) =>
            new Vector3
            {
                Z = original.Z,
                X = (float) Math.Abs(original.X + ChangeInX(distance, angle)),
                Y = (float) Math.Abs(original.Y + ChangeInY(distance, angle))
            };

        private static double ChangeInX(float distance, float angle) => distance * Math.Cos(angle);

        private static double ChangeInY(float distance, float angle) => distance * Math.Sin(angle);

        private static void SpawnCreaturesFromResRefs(int maxSpawns, IReadOnlyList<string> resRefs)
        {
            if (!resRefs.Any()) return;
                
            NWScript.ApplyEffectAtLocation(NWScript.DURATION_TYPE_INSTANT, NWScript.EffectVisualEffect(247), _spawnLocation);
            for (var i = 0; i < maxSpawns; i++)
            {
                var randomCreature = new Random().Next(0, resRefs.Count);
                SpawnEncounterAtWaypoint(resRefs[randomCreature]);
            }
        }

        private static void SpawnEncounterAtWaypoint(string resRef)
        {
            var creature = NWScript.CreateObject(NWScript.OBJECT_TYPE_CREATURE, resRef, _spawnLocation);
            if (creature == NWScript.OBJECT_INVALID) Console.WriteLine("Spawn wasn't valid!");
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