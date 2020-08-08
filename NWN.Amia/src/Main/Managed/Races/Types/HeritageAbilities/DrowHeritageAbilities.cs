using NWN.Amia.Main.Managed.Characters;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class DrowHeritageAbilities : IHeritageAbilities
    {
        
        public void SetupStats(Player player)
        {
            player.Dex += 2;
            player.Cha += 2;
            
            player.UpdateAbilities();
        }
    }
}