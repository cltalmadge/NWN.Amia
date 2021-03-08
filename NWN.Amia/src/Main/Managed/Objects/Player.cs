using System.Collections.Generic;
using JetBrains.Annotations;
using NWN.Amia.Main.Managed.Objects.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Objects
{
    public class Player : GameObject, ICreature 
    {
        public Player(uint objectId) : base(objectId)
        {

            Str = NWScript.GetAbilityScore(ObjectId, NWScript.ABILITY_STRENGTH);
            Con = NWScript.GetAbilityScore(ObjectId, NWScript.ABILITY_CONSTITUTION);
            Dex = NWScript.GetAbilityScore(ObjectId, NWScript.ABILITY_DEXTERITY);
            Int = NWScript.GetAbilityScore(ObjectId, NWScript.ABILITY_INTELLIGENCE);
            Wis = NWScript.GetAbilityScore(ObjectId, NWScript.ABILITY_WISDOM);
            Cha = NWScript.GetAbilityScore(ObjectId, NWScript.ABILITY_CHARISMA);

            PublicCdKey = NWScript.GetPCPublicCDKey(ObjectId);
            AccountName = NWScript.GetPCPlayerName(ObjectId);
            IpAddress = NWScript.GetPCIPAddress(ObjectId);

            HitDice = NWScript.GetHitDice(ObjectId);
            HitPoints = NWScript.GetMaxHitPoints(ObjectId);
            Subrace = NWScript.GetSubRace(ObjectId);
        }

        [UsedImplicitly] public string PublicCdKey { get; }

        [UsedImplicitly] public string AccountName { get; }
        [UsedImplicitly] public string IpAddress { get; }

        public string Subrace { get; }
        
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

        public void UpdateAbilities()
        {
            UpdateAbility(NWScript.ABILITY_STRENGTH, Str);
            UpdateAbility(NWScript.ABILITY_CONSTITUTION, Con);
            UpdateAbility(NWScript.ABILITY_DEXTERITY, Dex);
            UpdateAbility(NWScript.ABILITY_INTELLIGENCE, Int);
            UpdateAbility(NWScript.ABILITY_WISDOM, Wis);
            UpdateAbility(NWScript.ABILITY_CHARISMA, Cha);
        }

        private void UpdateAbility(int ability, int newAbilityValue)
        {
            CreaturePlugin.SetRawAbilityScore(ObjectId, ability, newAbilityValue);
        }
    }
}