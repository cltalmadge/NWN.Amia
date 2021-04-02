using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Utils;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Script.SubraceTemplates
{
    [ScriptName("race_init_kenk")]
    public class KenkuOption : IRunnableScript
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
                NWScript.SendMessageToPC(nwnObjectId, "Kenku only works with the Non-Regional Human base race.");
                return 0;
            }

            NWScript.CreateItemOnObject(TemplateItem.TemplateItemResRef, nwnObjectId);

            SetSubraceModifiers(nwnObjectId);

            var templateRunner = new TemplateRunner();
            CreaturePlugin.AddFeatByLevel(nwnObjectId,354,1);

            templateRunner.Run(nwnObjectId);
            
            return 0;
        }

        private static void SetSubraceModifiers(uint nwnObjectId)
        {
            TemplateItem.SetSubRace(nwnObjectId, "Kenku");
            TemplateItem.SetDexMod(nwnObjectId, 2);
            TemplateItem.SetStrMod(nwnObjectId, -2);
        }
    }
}