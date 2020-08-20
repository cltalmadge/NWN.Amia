using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("race_effects")]
    [UsedImplicitly]
    public class RaceEffects : IRunnableScript
    {
        private const string SubracePrefix = "subraceEffect";
        private static uint _player;

        public int Run(uint nwnObjectId)
        {
            _player = nwnObjectId;

            if (!RaceIsManaged()) return 0;

            RemoveTaggedEffects();
            SetEffectsToSupernaturalAndApply();

            return 0;
        }

        private static void RemoveTaggedEffects()
        {
            Effect effect = NWScript.GetFirstEffect(_player);

            while (NWScript.GetIsEffectValid(effect) == NWScript.TRUE)
            {
                if (NWScript.GetEffectTag(effect).Contains(SubracePrefix)) NWScript.RemoveEffect(_player, effect);

                effect = NWScript.GetNextEffect(_player);
            }
        }

        private static bool RaceIsManaged()
        {
            var playerRace = NWScript.GetRacialType(_player);

            return ManagedRaces.Races.ContainsKey(playerRace);
        }

        private static void SetEffectsToSupernaturalAndApply()
        {
            var supernaturalEffects = ConvertEffectsToSupernatural(GetListOfEffectsForRace());
            var taggedEffects = TagEffects(supernaturalEffects);

            foreach (var effect in taggedEffects) ApplyEffectPermanently(effect);
        }

        private static IEnumerable<Effect> ConvertEffectsToSupernatural(IEnumerable<Effect> raceEffects) =>
            raceEffects.Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy)
                .ToList();

        private static IEnumerable<Effect> GetListOfEffectsForRace()
        {
            var raceType = NWScript.GetRacialType(_player);

            var racialEffectCollector = ManagedRaces.Races[raceType];

            return null == racialEffectCollector
                ? new List<Effect>()
                : racialEffectCollector.GatherEffectsForObject(_player);
        }

        private static IEnumerable<Effect> TagEffects(IEnumerable<Effect> supernaturalEffects)
        {
            var taggedEffects = new List<Effect>();

            foreach (var effect in supernaturalEffects)
            {
                var subracePrefixWithCount = SubracePrefix + taggedEffects.Count;
                taggedEffects.Add(NWScript.TagEffect(effect, subracePrefixWithCount));
            }

            return taggedEffects;
        }

        private static void ApplyEffectPermanently(Effect effect) =>
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, _player);
    }
}