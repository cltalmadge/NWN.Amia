using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_air")]
    public class AirGenasiOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            if (TemplateItem.Initialized(nwnObjectId)) return 0;

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_OUTSIDER);

            SetSubRaceMod(nwnObjectId);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);

            return 0;
        }

        private static void SetSubRaceMod(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Air Genasi");
            TemplateItem.SetDexMod(nwnObjectId, 2);
            TemplateItem.SetIntMod(nwnObjectId, 2);
            TemplateItem.SetChaMod(nwnObjectId, -2);
            TemplateItem.SetWisMod(nwnObjectId, -2);
        }
    }
}