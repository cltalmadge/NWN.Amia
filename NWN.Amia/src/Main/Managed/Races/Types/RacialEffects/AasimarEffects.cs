using System;
using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class AasimarEffects : IEffectCollector
    {
        private const int Heritage = 0;
        private uint _oid = NWScript.OBJECT_INVALID;
        private bool _hasHeritageFeat;

        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            _oid = objectId;
            _hasHeritageFeat = HasHeritageFeat();

            var effects = new List<Effect>
            {
                NWScript.EffectSkillIncrease(NWScript.SKILL_PERSUADE, 2),
                NWScript.EffectDamageResistance(NWScript.DAMAGE_TYPE_COLD, 5),
                NWScript.EffectDamageResistance(NWScript.DAMAGE_TYPE_FIRE, 5),
                NWScript.EffectDamageResistance(NWScript.DAMAGE_TYPE_ELECTRICAL, 5)
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