﻿using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_feyri")]
    public class FeyriOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            if (TemplateItem.Initialized(nwnObjectId)) return 0;

            if (NWScript.GetItemPossessedBy(nwnObjectId, "platinum_token") == NWScript.OBJECT_INVALID)
            {
                NWScript.SendMessageToPC(nwnObjectId, "This subrace requires DM permission to play.");
                return 0;
            }

            if (NWScript.GetRacialType(nwnObjectId) != NWScript.RACIAL_TYPE_ELF)
            {
                NWScript.SendMessageToPC(nwnObjectId, "Fey'ri only works with the Moon Elf base race.");
                return 0;
            }

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubraceModifiers(nwnObjectId);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);
            CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_OUTSIDER);
            CreaturePlugin.AddFeatByLevel(nwnObjectId, 228, 1);
            
            return 0;
        }

        private static void SetSubraceModifiers(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Fey'ri");
        }
    }
}