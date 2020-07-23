﻿using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Types.RacialTemplates;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_fire")]
    public class FireGenasiOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubRaceMod(nwnObjectId);
            
            NWN.Core.NWNX.CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_OUTSIDER);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);

            return 0;
        }

        private static void SetSubRaceMod(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Fire Genasi");
            TemplateItem.SetIntMod(nwnObjectId, 2);
            TemplateItem.SetChaMod(nwnObjectId, -2);
        }
    }
}