using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Types.RacialEffects;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("subrace_effects"), UsedImplicitly]
    public class SubraceEffects : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            var listOfSubraceEffects = ResolveEffectsForObject(nwnObjectId);

            var applicator = new SubraceEffectApplicator(nwnObjectId, listOfSubraceEffects);

            applicator.Apply();

            return 0;
        }

        private List<Effect> ResolveEffectsForObject(uint nwnObjectId)
        {
            return NWScript.GetSubRace(nwnObjectId).ToLower() switch
            {
                "aasimar" => new AasimarEffects().GatherEffectsForObject(nwnObjectId),
                "tiefling" => new TieflingEffects().GatherEffectsForObject(nwnObjectId),
                "feyri" => new FeyriEffects().GatherEffectsForObject(nwnObjectId),
                "feytouched" => new FeytouchedEffects().GatherEffectsForObject(nwnObjectId),
                _ => new List<Effect>()
            };
        }
    }

    internal sealed class SubraceEffectApplicator
    {
        private static uint _nwnObjectId;
        private static List<Effect> _effects;

        public SubraceEffectApplicator(uint nwnObjectId, List<Effect> effects)
        {
            _nwnObjectId = nwnObjectId;
            _effects = effects;
        }

        public void Apply()
        {
            var supernaturalEffects = MakeEffectsSupernatural();

            foreach (var effect in supernaturalEffects)
            {
                NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, _nwnObjectId);
            }
        }

        private static IEnumerable<Effect> MakeEffectsSupernatural() => _effects
            .Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy).ToList();
    }
}