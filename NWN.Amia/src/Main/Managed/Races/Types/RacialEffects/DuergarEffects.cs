using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class DuergarEffects : IEffectCollector
    {
        private const int Heritage = 0;
        private bool _hasHeritageFeat;
        private uint _oid = NWScript.OBJECT_INVALID;

        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            _oid = objectId;
            _hasHeritageFeat = HasHeritageFeat();

            var effects = new List<Effect>
            {
                NWScript.EffectSkillIncrease(NWScript.SKILL_MOVE_SILENTLY, 4),
                NWScript.EffectSkillIncrease(NWScript.SKILL_LISTEN, 1),
                NWScript.EffectSkillIncrease(NWScript.SKILL_SPOT, 1),
                NWScript.EffectImmunity(NWScript.IMMUNITY_TYPE_PARALYSIS),
                NWScript.EffectImmunity(NWScript.IMMUNITY_TYPE_POISON),
                NWScript.EffectSpellImmunity(NWScript.SPELL_PHANTASMAL_KILLER)
            };

            AddHeritageEffectsIfObjectHasFeat(effects);

            return effects;
        }

        private bool HasHeritageFeat()
        {
            return NWScript.GetHasFeat(Heritage, _oid) == 1;
        }

        private void AddHeritageEffectsIfObjectHasFeat(ICollection<Effect> effectsForObject)
        {
            if (!_hasHeritageFeat) return;

            effectsForObject.Add(NWScript.EffectSavingThrowDecrease(NWScript.SAVING_THROW_ALL, 1));
        }
    }
}