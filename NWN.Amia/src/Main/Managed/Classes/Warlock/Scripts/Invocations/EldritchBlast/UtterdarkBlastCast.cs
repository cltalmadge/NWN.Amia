using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Spells.Commons;
using NWN.Amia.Main.Managed.Spells.Commons.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Classes.Warlock.Scripts.Invocations.EldritchBlast
{
    [ScriptName("wlk_uttrdrk_blast")]
    [UsedImplicitly]
    public class UtterdarkBlastCast:IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            uint spellTargetObject = NWScript.GetSpellTargetObject();

            bool targetFailedSpellResistance = SpellUtils.ResistSpell(nwnObjectId, spellTargetObject) == 0;
            bool touchAttackSucceeded = NWScript.TouchAttackRanged(spellTargetObject) > 0;
            if (!targetFailedSpellResistance || !touchAttackSucceeded) return 0;

            ICastable eldritchBlast = new Types.EldritchBlast(nwnObjectId, spellTargetObject,
                NWScript.DAMAGE_TYPE_NEGATIVE);
            eldritchBlast.CastSpell();
            WarlockHelper.ApplyUtterdarkEffects(nwnObjectId, spellTargetObject);

            return 0;
        }
    }
}