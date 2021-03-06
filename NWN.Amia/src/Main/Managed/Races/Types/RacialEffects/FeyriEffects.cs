﻿using System;
using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class FeyriEffects : IEffectCollector
    {
        public List<IntPtr> GatherEffectsForObject(uint objectId)
        {
            return new()
            {
                NWScript.EffectDamageResistance(NWScript.DAMAGE_TYPE_FIRE, 10)
            };
        }
    }
}