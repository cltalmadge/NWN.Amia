using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_aqelf")]
    public class AquaticElfOption : IRunnableScript
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
                NWScript.SendMessageToPC(nwnObjectId, "Aquatic Elf only works with the Moon Elf base race.");
                return 0;
            }

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubraceModifiers(nwnObjectId);

            var templateRunner = new TemplateRunner();

            templateRunner.Run(nwnObjectId);
            
            return 0;
        }

        private static void SetSubraceModifiers(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Aquatic Elf");
            TemplateItem.SetIntMod(nwnObjectId, -2);
            TemplateItem.SetDexMod(nwnObjectId, 2);
        }
    }
}