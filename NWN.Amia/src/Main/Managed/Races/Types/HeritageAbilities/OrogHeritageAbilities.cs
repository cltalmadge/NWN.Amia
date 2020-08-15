using System;
using NWN.Amia.Main.Managed.Characters;

namespace NWN.Amia.Main.Managed.Races.Types.HeritageAbilities
{
    public class OrogHeritageAbilities : IHeritageAbilities
    {
        public void SetupStats(Player player)
        {
            var str = player.Str;
            Console.WriteLine($"{str}");
            str += 1;
            Console.WriteLine($"{str}");
            player.Str += 1;
            player.Cha += 1;

            player.UpdateAbilities();
        }
    }
}