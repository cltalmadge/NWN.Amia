using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class DuergarEffects : IEffectCollector
    {
        private uint _oid = NWScript.OBJECT_INVALID;

        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            _oid = objectId;

            var effects = new List<Effect>
            {
                NWScript.EffectSkillIncrease(NWScript.SKILL_MOVE_SILENTLY, 4),
                NWScript.EffectSkillIncrease(NWScript.SKILL_LISTEN, 1),
                NWScript.EffectSkillIncrease(NWScript.SKILL_SPOT, 1),
                NWScript.EffectImmunity(NWScript.IMMUNITY_TYPE_PARALYSIS),
                NWScript.EffectImmunity(NWScript.IMMUNITY_TYPE_POISON),
                NWScript.EffectSpellImmunity(NWScript.SPELL_PHANTASMAL_KILLER)
            };


            return effects;
        }

    }
}