using System;
using System.Collections.Generic;
using NWN.Amia.Main.Managed.Classes.Warlock;
using NWN.Amia.Main.Managed.Spells.Commons.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Classes.Types
{
    public class EldritchBlast : ICastable
    {
        private readonly uint _caster;
        private readonly uint _target;
        private readonly int _elementConstant;
        private readonly int _beamVfx;
        private readonly int _impactVfxNumber;

        public EldritchBlast(uint caster, uint target, int elementConstant, int beamVfx = 0, int impactVfxNumber = 0)
        {
            _caster = caster;
            _target = target;
            _elementConstant = elementConstant;
            _beamVfx = beamVfx;
            _impactVfxNumber = impactVfxNumber;
        }

        public void CastSpell()
        {
            if (NWScript.GetIsReactionTypeHostile(_target, _caster) != NWScript.TRUE) return;

            WarlockHelper warlockHelper = new(_caster);

            int beamVfx = _beamVfx != 0 ? _beamVfx : WarlockHelper.GetBeamVfxNumberFromElement(_elementConstant);

            IntPtr rayVfx = NWScript.EffectBeam(beamVfx, _caster,
                NWScript.BODY_NODE_HAND);
            IntPtr impactVfx = _impactVfxNumber != 0 ? NWScript.EffectVisualEffect(_impactVfxNumber) : WarlockHelper.GetImpactVfxFromBeam(_beamVfx);

            int calculatedDamageBase = warlockHelper.CalculateBlastDice() * NWScript.d6();
            IntPtr damageEffect = NWScript.EffectDamage(calculatedDamageBase, _elementConstant);
             
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_TEMPORARY, rayVfx, _target, (float) 2.0);
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, impactVfx, _target);
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_INSTANT, damageEffect, _target);
        }
    }
}