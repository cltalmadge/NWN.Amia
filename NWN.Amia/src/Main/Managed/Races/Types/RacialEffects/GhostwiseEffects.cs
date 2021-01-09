using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class GhostwiseEffects : IEffectCollector
    {
        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            return new()
            {
                NWScript.EffectSkillDecrease(NWScript.SKILL_SPOT, 2),
                NWScript.EffectSkillDecrease(NWScript.SKILL_CONCENTRATION, 2),
                NWScript.EffectSkillIncrease(NWScript.SKILL_ANIMAL_EMPATHY, 2)
            };
        }
    }
}