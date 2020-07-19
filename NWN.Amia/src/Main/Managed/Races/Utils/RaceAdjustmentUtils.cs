using System.Collections.Generic;
using NWN.Amia.Main.Managed.Races.Types.RacialTemplates;

namespace NWN.Amia.Main.Managed.Races.Utils
{
public static class RaceAdjustmentUtils
{
    public static Dictionary<int, RacialTemplate> BaseRaceDict { get; }

    static RaceAdjustmentUtils()
    {
        BaseRaceDict = new Dictionary<int, RacialTemplate>
        {
            {(int) BaseRace.Human, GetHumanAdjustments()},
            {(int) BaseRace.Halfling, GetHalflingAdjustments()},
            {(int) BaseRace.Elf, GetElfAdjustments()},
            {(int) BaseRace.HalfOrc, GetHalfOrcAdjustments()},
            {(int) BaseRace.Gnome, GetGnomeAdjustments()},
            {(int) BaseRace.Dwarf, GetDwarfAdjustments()},
        };
    }

    private static RacialTemplate GetHalflingAdjustments()
    {
        return new RacialTemplate
        {
            StrBonus = 2,
            DexBonus = -2,
            ConBonus = 0,
            WisBonus = 0,
            IntBonus = 0,
            ChaBonus = 0
        };
    }

    private static RacialTemplate GetElfAdjustments()
    {
        return new RacialTemplate
        {
            StrBonus = 0,
            DexBonus = -2,
            ConBonus = 2,
            WisBonus = 0,
            IntBonus = 0,
            ChaBonus = 0
        };
    }

    private static RacialTemplate GetHalfOrcAdjustments()
    {
        return new RacialTemplate
        {
            StrBonus = -2,
            DexBonus = 0,
            ConBonus = 0,
            WisBonus = 0,
            IntBonus = 2,
            ChaBonus = 2
        };
    }

    private static RacialTemplate GetGnomeAdjustments()
    {
        return new RacialTemplate
        {
            StrBonus = 2,
            DexBonus = 0,
            ConBonus = -2,
            WisBonus = 0,
            IntBonus = 0,
            ChaBonus = 0
        };
    }

    private static RacialTemplate GetDwarfAdjustments()
    {
        return new RacialTemplate
        {
            StrBonus = 0,
            DexBonus = 0,
            ConBonus = -2,
            WisBonus = 0,
            IntBonus = 0,
            ChaBonus = 2
        };
    }

    private static RacialTemplate GetHumanAdjustments()
    {
        return new RacialTemplate
        {
            StrBonus = 0,
            IntBonus = 0,
            DexBonus = 0,
            ConBonus = 0,
            WisBonus = 0,
            ChaBonus = 0
        };
    }

    private enum BaseRace
    {
        Human = 0,
        Dwarf = 1,
        HalfOrc = 2,
        Elf = 3,
        Gnome = 4,
        Halfling = 5
    }
}
}