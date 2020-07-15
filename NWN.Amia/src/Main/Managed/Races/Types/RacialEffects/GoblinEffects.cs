using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class GoblinEffects : IEffectCollector
    {
        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            return new List<Effect>
            {
                NWScript.EffectSkillIncrease(NWScript.SKILL_MOVE_SILENTLY, 2),
                NWScript.EffectSkillIncrease(NWScript.SKILL_DISCIPLINE, 2)
            };
        }
    }
}