using NWN.Amia.Main.Managed.Characters;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class SvirfneblinHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            player.Wis += 2;

            player.UpdateAbilities();
        }
    }
}