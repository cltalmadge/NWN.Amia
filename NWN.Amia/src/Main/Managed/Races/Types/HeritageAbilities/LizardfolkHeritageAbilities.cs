﻿using NWN.Amia.Main.Managed.Objects;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class LizardfolkHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_STRENGTH, 2);
        }
    }
}