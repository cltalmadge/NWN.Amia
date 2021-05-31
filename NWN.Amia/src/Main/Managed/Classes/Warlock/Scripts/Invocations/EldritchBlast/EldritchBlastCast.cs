using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Spells.Commons;
using NWN.Amia.Main.Managed.Spells.Commons.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Classes.Warlock.Scripts.Invocations.EldritchBlast
{
    [ScriptName("wlk_eldr_blast"), UsedImplicitly]
    public class EldritchBlastCast : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            uint spellTargetObject = NWScript.GetSpellTargetObject();

            ICastable eldritchBlast = new Types.EldritchBlast(nwnObjectId, spellTargetObject,
                NWScript.DAMAGE_TYPE_MAGICAL);

            bool targetFailedSpellResistance = SpellUtils.ResistSpell(nwnObjectId, spellTargetObject) > 0;
            bool touchAttackSucceeded = NWScript.TouchAttackRanged(spellTargetObject) > 0;
            if (targetFailedSpellResistance && touchAttackSucceeded)
            {
                eldritchBlast.CastSpell();
            }

            return 0;
        }
    }
}