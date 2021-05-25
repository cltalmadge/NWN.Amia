using System;
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
            IntPtr effect = NWScript.GetFirstEffect(_player);

            while (NWScript.GetIsEffectValid(effect) == NWScript.TRUE)
            {
                if (NWScript.GetEffectTag(effect).Contains(SubracePrefix)) NWScript.RemoveEffect(_player, effect);

                effect = NWScript.GetNextEffect(_player);
            }
        }

        private static bool RaceIsManaged()
        {
            int playerRace = NWScript.GetRacialType(_player);

            return ManagedRaces.Races.ContainsKey(playerRace);
        }

        private static void SetEffectsToSupernaturalAndApply()
        {
            var supernaturalEffects = ConvertEffectsToSupernatural(GetListOfEffectsForRace());
            var taggedEffects = TagEffects(supernaturalEffects);

            foreach (var effect in taggedEffects) ApplyEffectPermanently(effect);
        }

        private static IEnumerable<IntPtr> ConvertEffectsToSupernatural(IEnumerable<IntPtr> raceEffects)
        {
            return raceEffects.Select(NWScript.SupernaturalEffect).Select(dummy => dummy)
                .ToList();
        }

        private static IEnumerable<IntPtr> GetListOfEffectsForRace()
        {
            var raceType = NWScript.GetRacialType(_player);

            var racialEffectCollector = ManagedRaces.Races[raceType];

            return null == racialEffectCollector
                ? new List<IntPtr>()
                : racialEffectCollector.GatherEffectsForObject(_player);
        }

        private static IEnumerable<IntPtr> TagEffects(IEnumerable<IntPtr> supernaturalEffects)
        {
            var taggedEffects = new List<IntPtr>();

            foreach (var effect in supernaturalEffects)
            {
                var subracePrefixWithCount = SubracePrefix + taggedEffects.Count;
                taggedEffects.Add(NWScript.TagEffect(effect, subracePrefixWithCount));
            }

            return taggedEffects;
        }

        private static void ApplyEffectPermanently(IntPtr effect)
        {
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, _player);
        }
    }
}