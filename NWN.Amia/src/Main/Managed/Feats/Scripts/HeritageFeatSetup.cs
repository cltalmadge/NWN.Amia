using System.Collections.Generic;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats.Scripts
{
    [ScriptName("heritage_setup"), UsedImplicitly]
    public class HeritageFeatSetup : IRunnableScript
    {
        private static uint _nwnObject;

        public int Run(uint nwnObjectId)
        {
            _nwnObject = nwnObjectId;

            if (PlayerRaceIsSupported() && HeritageFeatNotInitialized())
            {
                PerformHeritageFeatSetup(nwnObjectId);
            }

            return 0;
        }

        private static bool PlayerRaceIsSupported()
        {
            var playerRace = NWScript.GetRacialType(_nwnObject);
            return GetListOfRacesWithHeritage().Contains(playerRace);
        }

        private static bool HeritageFeatNotInitialized()
        {
            var pckey = NWScript.GetItemPossessedBy(_nwnObject, "ds_pckey");
            return NWScript.GetLocalInt(pckey, "heritage_setup") == NWScript.FALSE;
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

        private static void PerformHeritageFeatSetup(in uint nwnObjectId)
        {
            NWScript.SendMessageToPC(nwnObjectId, "TODO: Implement heritage abilities.");
        }
    }
}