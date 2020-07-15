using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races
{
    public class RacialEffectCreator : IFeatEffectCreator
    {
        public List<Effect> GetFeatEffects(uint objectId)
        {
            var raceType = NWScript.GetRacialType(objectId);

            var racialEffectCollector = RacialEffectDictionary.Races[raceType];

            return null == racialEffectCollector ? new List<Effect>() : racialEffectCollector.GatherEffectsForObject(objectId);
        }
    }
}