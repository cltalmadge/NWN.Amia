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
                {(int) RacialType.Ogrillon, new OgrillonEffects()},
                {(int)RacialType.Feytouched, new FeytouchedEffects()}
            };
        }

        public enum RacialType
        {
            Duergar = 1,
            Drow = 5,
            Tiefling = 1000,
            Aasimar = 1001,
            Svirfneblin = 9,
            Ghostwise = 11,
            Feyri = 1003,
            Goblin = 12,
            Kobold = 13,
            Hobgoblin = 18,
            Orc = 19,
            Orog = 21,
            Chultan = 24,
            Damaran = 25,
            Ffolk = 27,
            Mulan = 29,
            Ogrillon = 20,
            Feytouched = 1002
        }
    }
}