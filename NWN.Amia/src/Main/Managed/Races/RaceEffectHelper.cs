using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races
{
    public class RaceEffectHelper : IEffectCreator
    {
        public List<Effect> GetEffects(uint objectId)
        {
            var raceType = NWScript.GetRacialType(objectId);

            var racialEffectCollector = ManagedRaces.Races[raceType];

            return null == racialEffectCollector ? new List<Effect>() : racialEffectCollector.GatherEffectsForObject(objectId);
        }
    }
}