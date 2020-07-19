using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Amia.Main.Managed.Races.Types.RacialEffects;

namespace NWN.Amia.Main.Managed.Races
{
    public static class ManagedRaces
    {
        public static Dictionary<int, IEffectCollector> Races { get; }

        static ManagedRaces()
        {
            Races = new Dictionary<int, IEffectCollector>
            {
                {(int) RacialType.Duergar, new DuergarEffects()},
                {(int) RacialType.Drow, new DrowEffects()},
                {(int) RacialType.Tiefling, new TieflingEffects()},
                {(int) RacialType.Aasimar, new AasimarEffects()},
                {(int) RacialType.Svirfneblin, new SvirfneblinEffects()},
                {(int) RacialType.Ghostwise, new GhostwiseEffects()},
                {(int) RacialType.Feyri, new FeyriEffects()},
                {(int) RacialType.Goblin, new GoblinEffects()},
                {(int) RacialType.Kobold, new KoboldEffects()},
                {(int) RacialType.Hobgoblin, new HobgoblinEffects()},
                {(int) RacialType.Orc, new OrcEffects()},
                {(int) RacialType.Orog, new OrogEffects()},
                {(int) RacialType.Chultan, new ChultanEffects()},
                {(int) RacialType.Damaran, new DamaranEffects()},
                {(int) RacialType.Ffolk, new FfolkEffects()},
                {(int) RacialType.Mulan, new MulanEffects()},
                {(int) RacialType.Ogrillon, new OgrillonEffects()}
            };
        }

        public enum RacialType
        {
            Duergar = 0,
            Drow = 1,
            Tiefling = 2,
            Aasimar = 3,
            Svirfneblin = 4,
            Ghostwise = 5,
            Feyri = 6,
            Goblin = 8,
            Kobold = 9,
            Hobgoblin = 10,
            Orc = 11,
            Orog = 12,
            Chultan = 13,
            Damaran = 14,
            Ffolk = 15,
            Mulan = 16,
            Ogrillon = 17,
            Feytouched = 18
        }
    }
}