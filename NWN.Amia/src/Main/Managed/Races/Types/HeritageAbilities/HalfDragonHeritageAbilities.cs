using NWN.Amia.Main.Managed.Objects;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class HalfDragonHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_CONSTITUTION, 2);
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_CHARISMA, 2);
        }
    }
}