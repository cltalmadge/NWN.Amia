using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class FeyriEffects : IEffectCollector
    {
        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            return new List<Effect>
            {
                NWScript.EffectDamageResistance(NWScript.DAMAGE_TYPE_FIRE, 10)
            };
        }
    }
}