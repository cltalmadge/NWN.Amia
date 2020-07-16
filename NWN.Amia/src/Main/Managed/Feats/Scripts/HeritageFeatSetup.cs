using System;
using System.Collections.Generic;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Feats.Scripts
{
    [ScriptName("heritage_setup")]
    public class HeritageFeatSetup : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            var playerRace = NWScript.GetRacialType(nwnObjectId);
            var pckey = NWScript.GetItemPossessedBy(nwnObjectId, "ds_pckey");
            
            if (!GetListOfRacesWithHeritage().Contains(playerRace)) return 0;
            if (NWScript.GetLocalInt(pckey, "heritage_setup") == 1) return 0;

            PerformHeritageFeatSetup(nwnObjectId);
                
            return 0;
        }

        private static List<int> GetListOfRacesWithHeritage()
        {
            return new List<int>
            {
                (int) ManagedRaces.RacialType.Aasimar,
                (int) ManagedRaces.RacialType.Drow,
                (int) ManagedRaces.RacialType.Tiefling,
                (int) ManagedRaces.RacialType.Svirfneblin,
                (int) ManagedRaces.RacialType.Feyri,
                (int) ManagedRaces.RacialType.Orog,
                (int) ManagedRaces.RacialType.Ogrillon,
                (int) ManagedRaces.RacialType.Feytouched
            };
        }

        private void PerformHeritageFeatSetup(in uint nwnObjectId)
        {
            NWScript.SendMessageToPC(nwnObjectId, "TODO: Implement heritage abilities.");
        }
    }
}