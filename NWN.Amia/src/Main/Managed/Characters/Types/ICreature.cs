using System.Collections.Generic;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Characters.Types
{
    public interface ICreature
    {
        public uint ObjectId { get; set; }
        
        public int Str { get; set; }
        public int Int { get; set; }
        public int Con { get; set; }
        public int Cha { get; set; }
        public int Wis { get; set; }
        public int Dex { get; set; }
        
        public int HitDice { get; set; }
        public int HitPoints { get; set; }
        
        public List<int> Classes { get; set; }
        public List<Effect> ActiveEffects { get; set; }
    }
}