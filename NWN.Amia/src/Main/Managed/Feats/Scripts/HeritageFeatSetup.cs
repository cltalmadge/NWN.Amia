using System.Collections.Generic;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats.Scripts
{
    [ScriptName("heritage_setup")]
    public class HeritageFeatSetup : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            var playerRace = NWScript.GetRacialType(nwnObjectId);

            if (!GetListOfRacesWithHeritage().Contains(playerRace)) return 0;

         
            
                
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
    }
}