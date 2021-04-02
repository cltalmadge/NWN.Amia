using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_liz")]
    public class LizardfolkOption : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            if (TemplateItem.Initialized(nwnObjectId)) return 0;

            if (NWScript.GetItemPossessedBy(nwnObjectId, "platinum_token") == NWScript.OBJECT_INVALID)
            {
                NWScript.SendMessageToPC(nwnObjectId, "This subrace requires DM permission to play.");
                return 0;
            }

            if (NWScript.GetRacialType(nwnObjectId) != NWScript.RACIAL_TYPE_HUMAN)
            {
                NWScript.SendMessageToPC(nwnObjectId, "Lizardfolk only works with the Non-Regional Human base race.");
                return 0;
            }

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubraceModifiers(nwnObjectId);

            var templateRunner = new TemplateRunner();
            CreaturePlugin.SetRacialType(nwnObjectId, NWScript.RACIAL_TYPE_HUMANOID_REPTILIAN);

            templateRunner.Run(nwnObjectId);
            
            return 0;
        }

        private static void SetSubraceModifiers(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Lizardfolk");
            TemplateItem.SetConMod(nwnObjectId, 2);
            TemplateItem.SetIntMod(nwnObjectId, -2);
        }
    }
}