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

        public int Run(uint nwnObjectId)
        {
            _trigger = nwnObjectId;

            var player = NWScript.GetEnteringObject();
            var area = NWScript.GetArea(_trigger);

            var notPlayer = NWScript.GetIsPC(player) != 1 && NWScript.GetIsPossessedFamiliar(player) != 1;
            if (notPlayer) return 0;


            if (TriggerStillOnCooldown())
            {
                NWScript.SendMessageToPC(player, "You see signs of recent fighting here.");
                return 0;
            }

            var spawner = new DayNightEncounterSpawner(_trigger, area, player);

            if (GetNumberOfPartyMembers(player) > 6) spawner.DoubleSpawn = true;

            spawner.SpawnEncounters();

            InitTriggerCooldown();

            return 0;
        }

        private bool TriggerStillOnCooldown()
        {
            return TimePlugin.GetTimeStamp() - GetTriggerCoolDownStart() <= 900;
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
                if (NWScript.GetIsPC(partyMember) == NWScript.TRUE) partyMembers++;

                partyMember = NWScript.GetNextFactionMember(player);
            }

            return partyMembers;
        }

        private void InitTriggerCooldown()
        {
            NWScript.SetLocalInt(_trigger, "cooldown_start", TimePlugin.GetTimeStamp());
        }
    }
}