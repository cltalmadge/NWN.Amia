﻿using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_tief")]
    public class TieflingOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            if (TemplateItem.Initialized(nwnObjectId)) return 0;

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubRaceMod(nwnObjectId);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);

            CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_OUTSIDER);
            CreaturePlugin.AddFeatByLevel(nwnObjectId,0,1);//TODO:Change feat number to Darkvision.

            return 0;
        }

        private static void SetSubRaceMod(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Tiefling");
            TemplateItem.SetIntMod(nwnObjectId, 2);
            TemplateItem.SetChaMod(nwnObjectId, -2);
        }
    }
}