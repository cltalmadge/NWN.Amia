using NWN.Amia.Main.Managed.Characters;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class OgrillonHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            player.Str += 1;

            player.UpdateAbilities();
        }
    }
}