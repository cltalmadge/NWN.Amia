using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("race_effects"), UsedImplicitly]
    public class RaceEffects : IRunnableScript
    {
        private static uint _player;

        public int Run(uint nwnObjectId)
        {
            _player = nwnObjectId;

            if (RaceIsManaged()) SetEffectsToSupernaturalAndApply();

            return 0;
        }

        private static bool RaceIsManaged()
        {
            var playerRace = NWScript.GetRacialType(_player);

            return ManagedRaces.Races.ContainsKey(playerRace);
        }

        private void SetEffectsToSupernaturalAndApply()
        {
            var supernaturalEffects = ConvertEffectsToSupernatural(GetListOfEffectsForRace());

            foreach (var effect in supernaturalEffects)
            {
                ApplyEffectPermanently(effect);
            }
        }

        private static void ApplyEffectPermanently(Effect effect) =>
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, _player);

        private static List<Effect> GetListOfEffectsForRace() => new RacialEffectCreator().GetFeatEffects(_player);

        private IEnumerable<Effect> ConvertEffectsToSupernatural(IEnumerable<Effect> raceEffects) =>
            raceEffects.Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy)
                .ToList();
    }
}