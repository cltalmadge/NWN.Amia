﻿using System.Collections.Generic;
using NWN.Amia.Main.Managed.Races.Types.RacialTemplates;

namespace NWN.Amia.Main.Managed.Races.Utils
{
    public static class RaceAdjustmentUtils
    {
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
                {(int) BaseRace.HalfElf, GetHalfElfAdjustments()}
            };
        }

        public static Dictionary<int, RacialTemplate> BaseRaceDict { get; }

        private static RacialTemplate GetHalflingAdjustments()
        {
            return new RacialTemplate
            {
                StrBonus = 0,
                DexBonus = 0,
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
                DexBonus = 0,
                ConBonus = 0,
                WisBonus = 0,
                IntBonus = 0,
                ChaBonus = 0
            };
        }

        private static RacialTemplate GetHalfOrcAdjustments()
        {
            return new RacialTemplate
            {
                StrBonus = 0,
                DexBonus = 0,
                ConBonus = 0,
                WisBonus = 0,
                IntBonus = 0,
                ChaBonus = 0
            };
        }

        private static RacialTemplate GetGnomeAdjustments()
        {
            return new RacialTemplate
            {
                StrBonus = 0,
                DexBonus = 0,
                ConBonus = 0,
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
                ConBonus = 2,
                WisBonus = 0,
                IntBonus = 0,
                ChaBonus = 0
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

        private static RacialTemplate GetHalfElfAdjustments()
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
            Human = 6,
            Dwarf = 0,
            HalfOrc = 5,
            HalfElf = 4,
            Elf = 1,
            Gnome = 2,
            Halfling = 3
        }
    }
}