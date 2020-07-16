using System.Collections.Generic;
using System.Linq;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("race_abilities")]
    public class RaceAbilities : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            // Race isn't managed. Do nothing.
            var playerRace = NWScript.GetSubRace(nwnObjectId) == ""
                ? NWScript.GetRacialType(nwnObjectId)
                : GetRaceFromSubrace(nwnObjectId);

            var raceIsNotManaged = !ManagedRaces.Races.ContainsKey(playerRace);
            if (raceIsNotManaged)
            {
                return 0;
            }
            
            SetEffectsToSupernaturalAndApply(nwnObjectId);

            return 0;
        }

        private void SetEffectsToSupernaturalAndApply(uint nwnObjectId)
        {
            var raceEffects = GetListOfEffectsForRace(nwnObjectId);
            var supernaturalEffects = ConvertEffectsToSupernatural(raceEffects);

            foreach (Effect effect in supernaturalEffects)
            {
                NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, nwnObjectId);
            }
        }

        private int GetRaceFromSubrace(in uint nwnObjectId)
        {
            return NWScript.GetSubRace(nwnObjectId).ToLower() switch
            {
                "aasimar" => (int) ManagedRaces.RacialType.Aasimar,
                "tiefling" => (int) ManagedRaces.RacialType.Tiefling,
                "feytouched" => (int) ManagedRaces.RacialType.Feytouched,
                "fey'ri" => (int) ManagedRaces.RacialType.Feyri,
                "feyri" => (int) ManagedRaces.RacialType.Feyri,
                _ => -1
            };
        }

        private static List<Effect> GetListOfEffectsForRace(uint nwnObjectId)
        {
            var raceEffects = new RacialEffectCreator().GetFeatEffects(nwnObjectId);
            return raceEffects;
        }

        private List<Effect> ConvertEffectsToSupernatural(List<Effect> raceEffects)
        {
            return raceEffects.Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy)
                .ToList();
        }
    }
}