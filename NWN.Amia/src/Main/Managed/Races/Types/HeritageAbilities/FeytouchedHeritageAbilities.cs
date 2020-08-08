using NWN.Amia.Main.Managed.Characters;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class FeytouchedHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            player.Dex += 2;

            player.UpdateAbilities();
        }
    }
}