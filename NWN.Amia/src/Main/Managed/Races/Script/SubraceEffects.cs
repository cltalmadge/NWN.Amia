using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Feats;
using NWN.Amia.Main.Managed.Races.Types.RacialEffects;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("subrace_effects")]
    [UsedImplicitly]
    public class SubraceEffects : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            var listOfSubraceEffects = ResolveEffectsForObject(nwnObjectId);

            if (!listOfSubraceEffects.Any()) return 0;

            EffectApplier.Apply(nwnObjectId, listOfSubraceEffects);

            return 0;
        }

        private static List<IntPtr> ResolveEffectsForObject(uint nwnObjectId)
        {
            return NWScript.GetSubRace(nwnObjectId).ToLower() switch
            {
                "aasimar" => new AasimarEffects().GatherEffectsForObject(nwnObjectId),
                "tiefling" => new TieflingEffects().GatherEffectsForObject(nwnObjectId),
                "fey'ri" => new FeyriEffects().GatherEffectsForObject(nwnObjectId),
                "feyri" => new FeyriEffects().GatherEffectsForObject(nwnObjectId),
                "feytouched" => new FeytouchedEffects().GatherEffectsForObject(nwnObjectId),
                "centaur" => new CentaurEffects().GatherEffectsForObject(nwnObjectId),
                "avariel" => new AvarielEffects().GatherEffectsForObject(nwnObjectId),
                "half dragon" => new HalfDragonEffects().GatherEffectsForObject(nwnObjectId),
                "half-dragon" => new HalfDragonEffects().GatherEffectsForObject(nwnObjectId),
                "dragon" => new HalfDragonEffects().GatherEffectsForObject(nwnObjectId),
                _ => new List<IntPtr>()
            };
        }
    }
}