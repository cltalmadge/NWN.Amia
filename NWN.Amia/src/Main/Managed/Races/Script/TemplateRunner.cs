using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Types.RacialTemplates;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("char_templates")]
    public class TemplateRunner : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            if (TemplateItem.CreatureDoesNotHaveTemplate(nwnObjectId)) return 0;
            if (TemplateItem.Initialized(nwnObjectId)) return 0;

            var template = TemplateMaker.CreateTemplate(nwnObjectId);

            template.Apply();

            return 0;
        }
    }

    public static class TemplateMaker
    {
        public static ICharacterTemplate CreateTemplate(in uint nwnObjectId)
        {
            var templateItem = NWScript.GetItemPossessedBy(nwnObjectId, TemplateItem.TemplateItemResRef);

            return new RacialTemplate(nwnObjectId)
            {
                StrBonus = NWScript.GetLocalInt(templateItem, "str_mod"),
                DexBonus = NWScript.GetLocalInt(templateItem, "dex_mod"),
                ConBonus = NWScript.GetLocalInt(templateItem, "con_mod"),
                IntBonus = NWScript.GetLocalInt(templateItem, "int_mod"),
                WisBonus = NWScript.GetLocalInt(templateItem, "wis_mod"),
                ChaBonus = NWScript.GetLocalInt(templateItem, "cha_mod"),
                SubRace = NWScript.GetLocalString(templateItem, "subrace")
            };
        }
    }
}