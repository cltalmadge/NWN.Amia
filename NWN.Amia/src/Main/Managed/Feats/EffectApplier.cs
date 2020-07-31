using System.Collections.Generic;
using System.Linq;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats
{
    public static class EffectApplier
    {
        public static void Apply(uint nwnObjectId, List<Effect> effects)
        {
            var supernaturalEffects = MakeEffectsSupernatural(effects);

            foreach (var effect in supernaturalEffects)
            {
                NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, nwnObjectId);
            }
        }

        private static IEnumerable<Effect> MakeEffectsSupernatural(List<Effect> effects) => effects
            .Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy).ToList();
    }
}