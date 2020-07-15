using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class OrogEffects : IEffectCollector
    {
        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            return new List<Effect>
            {
                NWScript.EffectSkillIncrease(NWScript.SKILL_CRAFT_ARMOR, 2),
                NWScript.EffectSkillIncrease(NWScript.SKILL_CRAFT_WEAPON, 2),
                NWScript.EffectDamageResistance(NWScript.DAMAGE_TYPE_FIRE, 5),
                NWScript.EffectDamageResistance(NWScript.DAMAGE_TYPE_COLD, 5),
            };
        }
    }
}