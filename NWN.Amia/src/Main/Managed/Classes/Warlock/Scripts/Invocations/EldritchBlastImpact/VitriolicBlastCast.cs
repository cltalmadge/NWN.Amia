using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Spells.Commons.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Classes.Warlock.Scripts.Invocations.EldritchBlastImpact
{
    public class VitriolicBlastCast : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            uint spellTargetObject = NWScript.GetSpellTargetObject();

            int touchAttackRanged = NWScript.TouchAttackRanged(spellTargetObject);
            bool touchAttackSucceeded = touchAttackRanged > 0;
            if (!touchAttackSucceeded) return 0;

            ICastable eldritchBlast = new Types.EldritchBlast(nwnObjectId, spellTargetObject,
                NWScript.DAMAGE_TYPE_MAGICAL, NWScript.VFX_BEAM_BLACK, 645, touchAttackRanged == 2);
            eldritchBlast.CastSpell();
            WarlockHelper.ApplyVitriolicEffects(nwnObjectId, spellTargetObject);

            return 0;
        }
    }
}