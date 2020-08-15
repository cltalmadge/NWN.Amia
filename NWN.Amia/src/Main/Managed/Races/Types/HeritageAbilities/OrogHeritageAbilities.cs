using System;
using NWN.Amia.Main.Managed.Characters;
using NWN.Core;
using NWN.Core.NWNX;

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
            
            // Hack
            CreaturePlugin.ModifyRawAbilityScore(player.ObjectId, NWScript.ABILITY_STRENGTH, -1);
        }
    }
}