using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_aas")]
    public class AasimarOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            if (TemplateItem.Initialized(nwnObjectId)) return 0;

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubraceMods(nwnObjectId);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);
            
            CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_OUTSIDER);

            return 0;
        }

        private static void SetSubraceMods(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Aasimar");
            TemplateItem.SetWisMod(nwnObjectId, 2);
            TemplateItem.SetConMod(nwnObjectId, -2);
        }
    }
}