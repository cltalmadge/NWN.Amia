using NWN.Amia.Main.Managed.Characters;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class OgrillonHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            var playerId = player.ObjectId;
            CreaturePlugin.ModifyRawAbilityScore(playerId, NWScript.ABILITY_STRENGTH, 1);
        }
    }
}