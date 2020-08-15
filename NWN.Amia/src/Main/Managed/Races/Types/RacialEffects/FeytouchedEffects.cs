using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class FeytouchedEffects : IEffectCollector
    {
        private const int Heritage = 0;
        private bool _hasHeritageFeat;
        private uint _oid = NWScript.OBJECT_INVALID;

        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            _oid = objectId;
            _hasHeritageFeat = HasHeritageFeat();

            var effectsForObject = new List<Effect>();

            AddHeritageEffectsIfObjectHasFeat(effectsForObject);

            return effectsForObject;
        }

        private bool HasHeritageFeat()
        {
            return NWScript.GetHasFeat(Heritage, _oid) == 1;
        }

        private void AddHeritageEffectsIfObjectHasFeat(List<Effect> effectsForObject)
        {
            if (!_hasHeritageFeat) return;

            effectsForObject.Add(NWScript.EffectSavingThrowIncrease(NWScript.SAVING_THROW_WILL, 1));
            effectsForObject.Add(NWScript.EffectSavingThrowDecrease(NWScript.SAVING_THROW_FORT, 1));
            effectsForObject.Add(NWScript.EffectSavingThrowDecrease(NWScript.SAVING_THROW_REFLEX, 1));
        }
    }
}