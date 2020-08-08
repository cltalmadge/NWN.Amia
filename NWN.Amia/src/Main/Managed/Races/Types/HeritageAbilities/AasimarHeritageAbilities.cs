using NWN.Amia.Main.Managed.Characters;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class AasimarHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            player.Cha += 2;

            player.UpdateAbilities();
        }
    }
}