using NWN.Amia.Main.Managed.Objects;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class AvarielHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_WISDOM, 1);
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_INTELLIGENCE, 1);
        }
    }
}