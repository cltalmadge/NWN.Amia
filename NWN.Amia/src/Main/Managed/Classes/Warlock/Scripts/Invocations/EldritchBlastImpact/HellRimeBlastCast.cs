﻿using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Spells.Commons;
using NWN.Amia.Main.Managed.Spells.Commons.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Classes.Warlock.Scripts.Invocations.EldritchBlastImpact
{
    [ScriptName("wlk_hllrme_blst")]
    [UsedImplicitly]
    public class HellRimeBlastCast : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            uint spellTargetObject = NWScript.GetSpellTargetObject();

            bool targetFailedSpellResistance = SpellUtils.ResistSpell(nwnObjectId, spellTargetObject) == 0;
            int touchAttackRanged = NWScript.TouchAttackRanged(spellTargetObject);
            bool touchAttackSucceeded = touchAttackRanged > 0;
            if (!targetFailedSpellResistance || !touchAttackSucceeded) return 0;

            ICastable eldritchBlast = new Types.EldritchBlast(nwnObjectId, spellTargetObject,
                NWScript.DAMAGE_TYPE_COLD, touchAttackRanged == 2);
            eldritchBlast.CastSpell();
            WarlockHelper.ApplyHellrimeEffects(nwnObjectId, spellTargetObject);

            return 0;
        }
    }
}