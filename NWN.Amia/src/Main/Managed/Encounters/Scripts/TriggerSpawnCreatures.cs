using System;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Encounters.Scripts
{
    [ScriptName("ds_db_spawner")]
    [UsedImplicitly]
    public class TriggerSpawnCreatures : IRunnableScript
    {
        private uint _trigger;
        private const int FifteenMinutesSeconds = 900;

        public int Run(uint nwnObjectId)
        {
            _trigger = nwnObjectId;

            var player = NWScript.GetEnteringObject();
            var area = NWScript.GetArea(_trigger);

            var notPlayer = NWScript.GetIsPC(player) != NWScript.TRUE &&
                            NWScript.GetIsPossessedFamiliar(player) != NWScript.TRUE;
            if (notPlayer) return 0;

            if (TriggerStillOnCooldown() && NWScript.GetLocalInt(_trigger, "on_cooldown") == NWScript.TRUE)
            {
                NWScript.SendMessageToPC(player, "You see signs of recent fighting here.");
                return 0;
            }

            var spawner = new DayNightEncounterSpawner(_trigger, area);

            if (GetNumberOfPartyMembers(player) > 6) spawner.IsDoubleSpawn = true;

            spawner.SpawnEncounters();

            InitTriggerCooldown();
            NWScript.DelayCommand(FifteenMinutesSeconds,
                () => NWScript.SetLocalInt(_trigger, "on_cooldown", NWScript.TRUE));

            return 0;
        }

        private bool TriggerStillOnCooldown()
        {
            return (int) DateTimeOffset.Now.ToUnixTimeSeconds() - GetTriggerCoolDownStart() <= FifteenMinutesSeconds;
        }

        private int GetTriggerCoolDownStart()
        {
            return NWScript.GetLocalInt(_trigger, "cooldown_start");
        }

        private static int GetNumberOfPartyMembers(in uint player)
        {
            var partyMembers = 0;

            var partyMember = NWScript.GetFirstFactionMember(player);

            while (NWScript.GetIsObjectValid(partyMember) == NWScript.TRUE)
            {
                if (NWScript.GetIsPC(partyMember) == NWScript.TRUE &&
                    NWScript.GetArea(partyMember) == NWScript.GetArea(player)) partyMembers++;

                partyMember = NWScript.GetNextFactionMember(player);
            }

            return partyMembers;
        }

        private void InitTriggerCooldown()
        {
            NWScript.SetLocalInt(_trigger, "cooldown_start", (int) DateTimeOffset.Now.ToUnixTimeSeconds());
            NWScript.WriteTimestampedLogEntry($"{NWScript.GetLocalInt(_trigger, "cooldown_start")}");
        }
    }
}