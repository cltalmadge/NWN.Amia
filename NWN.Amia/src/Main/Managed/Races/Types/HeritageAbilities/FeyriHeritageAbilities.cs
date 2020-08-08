using NWN.Amia.Main.Managed.Characters;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class FeyriHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            player.Int += 2;

            player.UpdateAbilities();
        }
    }
}