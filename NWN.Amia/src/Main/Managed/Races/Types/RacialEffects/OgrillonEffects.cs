﻿using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class OgrillonEffects : IEffectCollector
    {
        private uint _oid = NWScript.OBJECT_INVALID;
        private bool _hasHeritageFeat;
        private const int Heritage = 0;

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

            effectsForObject.Add(NWScript.EffectACIncrease(2, NWScript.AC_DODGE_BONUS));
        }
    }
}