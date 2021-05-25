using System;
using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class FfolkEffects : IEffectCollector
    {
        public List<IntPtr> GatherEffectsForObject(uint objectId)
        {
            return new()
            {
                NWScript.EffectSkillIncrease(NWScript.SKILL_ANIMAL_EMPATHY, 2),
                NWScript.EffectSkillIncrease(NWScript.SKILL_LORE, 2)
            };
        }
    }
}