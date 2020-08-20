using System.Collections.Generic;
using System.Linq;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats
{
    public static class EffectApplier
    {
        private static uint _player;
        private const string SubracePrefix = "subraceEffect";

        public static void Apply(uint nwnObjectId, List<Effect> effects)
        {
            _player = nwnObjectId;

            RemoveTaggedEffects();

            SetEffectsToSupernaturalAndApply(effects);
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

        private static void SetEffectsToSupernaturalAndApply(IEnumerable<Effect> effects)
        {
            var supernaturalEffects = ConvertEffectsToSupernatural(effects);
            var taggedEffects = TagEffects(supernaturalEffects);

            foreach (var effect in taggedEffects) ApplyEffectPermanently(effect);
        }

        private static IEnumerable<Effect> ConvertEffectsToSupernatural(IEnumerable<Effect> raceEffects) =>
            raceEffects.Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy)
                .ToList();


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