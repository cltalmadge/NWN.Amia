﻿using System.Collections.Generic;
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

        private static void SetEffectsToSupernaturalAndApply()
        {
            var supernaturalEffects = ConvertEffectsToSupernatural(GetListOfEffectsForRace());

            foreach (var effect in supernaturalEffects)
            {
                if (NWScript.GetIsEffectValid(effect) == NWScript.TRUE) RemoveEffectIfExists(effect);
                ApplyEffectPermanently(effect);
            }
        }

        private static IEnumerable<Effect> GetListOfEffectsForRace()
        {
            var raceType = NWScript.GetRacialType(_player);

            var racialEffectCollector = ManagedRaces.Races[raceType];

            return null == racialEffectCollector
                ? new List<Effect>()
                : racialEffectCollector.GatherEffectsForObject(_player);
        }

        private static IEnumerable<Effect> ConvertEffectsToSupernatural(IEnumerable<Effect> raceEffects) =>
            raceEffects.Select(effect => NWScript.SupernaturalEffect(effect)).Select(dummy => (Effect) dummy)
                .ToList();

        private static void RemoveEffectIfExists(Effect effect) => NWScript.RemoveEffect(_player, effect);

        private static void ApplyEffectPermanently(Effect effect) =>
            NWScript.ApplyEffectToObject(NWScript.DURATION_TYPE_PERMANENT, effect, _player);
    }
}