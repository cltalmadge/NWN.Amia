using System.Collections.Generic;
using NWN.Amia.Main.Managed.Feats.Types;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types.RacialEffects
{
    public class CentaurEffects : IEffectCollector
    {
        private uint _oid = NWScript.OBJECT_INVALID;

        public List<Effect> GatherEffectsForObject(uint objectId)
        {
            _oid = objectId;

            var effects = new List<Effect>
            {
                NWScript.EffectSkillIncrease(NWScript.SKILL_MOVE_SILENTLY, 2),
            };


            return effects;
        }

    }
}