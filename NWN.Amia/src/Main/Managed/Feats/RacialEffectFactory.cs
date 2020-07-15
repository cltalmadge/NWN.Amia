using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.VisualBasic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats
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