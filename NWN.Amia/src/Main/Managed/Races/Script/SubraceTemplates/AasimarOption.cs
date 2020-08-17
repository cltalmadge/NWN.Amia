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
            
            // Bodge. I have no earthly idea as to why NWNX's Creature Plugin says it doesn't account for racial bonuses but then does it anyways. (e.g. decrement by 1, end up getting decremented by 2, instead).
            

            templateRunner.Run(nwnObjectId);

            if (NWScript.GetRacialType(nwnObjectId) == NWScript.RACIAL_TYPE_HALFELF)
            {
                CreaturePlugin.ModifyRawAbilityScore(nwnObjectId, NWScript.ABILITY_DEXTERITY, 1);
            }
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