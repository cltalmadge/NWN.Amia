using NWN.Amia.Main.Managed.Characters;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class DrowHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_DEXTERITY, 2);
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_CHARISMA, 2);

        }
    }
}