using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class MulanEffects : IEffectCollector
    {
        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            return new List<Effect>
            {
                NWScript.EffectSavingThrowDecrease(NWScript.SAVING_THROW_WILL, 1)
            };
        }
    }
}