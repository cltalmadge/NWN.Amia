using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_fey")]
    public class FeytouchedOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            if (TemplateItem.Initialized(nwnObjectId)) return 0;

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubRaceMod(nwnObjectId);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);

            CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_FEY);

            return 0;
        }

        private static void SetSubRaceMod(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Feytouched");
            TemplateItem.SetChaMod(nwnObjectId, 2);
            TemplateItem.SetConMod(nwnObjectId, -2);
        }
    }
}