using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Types.RacialTemplates;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_fey")]
    public class FeytouchedOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubRaceMod(nwnObjectId);
            
            NWN.Core.NWNX.CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_FEY);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);

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