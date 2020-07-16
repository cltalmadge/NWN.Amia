using System.Collections.Generic;
using System.Linq;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("race_abilities")]
    public class RaceAbilities : IRunnableScript
    {
        public List<Effect> AppliedEffects { get; set; }

        public int Run(uint nwnObjectId)
        {
            // Race isn't managed. Do nothing.
            if (!ManagedRaces.Races.ContainsKey(NWScript.GetRacialType(nwnObjectId)))
            {
                return 0;
            }

            var raceEffects = new RacialEffectCreator().GetFeatEffects(nwnObjectId);
            var supernaturalEffects = ConvertEffectsToSupernatural(raceEffects);

            foreach (Effect effect in supernaturalEffects)
            {
                NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, nwnObjectId);
            }

            return 0;
        }

        private List<Effect> ConvertEffectsToSupernatural(List<Effect> raceEffects)
        {
            return raceEffects.Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy)
                .ToList();
        }
    }
}