using NWN.Amia.Main.Managed.Characters;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class AasimarHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_CHARISMA, 2);

        }
    }
}