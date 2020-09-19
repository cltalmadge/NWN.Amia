using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Utils
{
    public static class TemplateItem
    {
        public const string TemplateItemResRef = "char_template";

        private const string StrModVar = "str_mod";
        private const string DexModVar = "dex_mod";
        private const string ConModVar = "con_mod";
        private const string IntModVar = "int_mod";
        private const string WisModVar = "wis_mod";
        private const string ChaModVar = "cha_mod";
        private const string SubRaceVar = "subrace";

        public const string InitializedVar = "template_initialized";

        public static bool CreatureDoesNotHaveTemplate(uint creature)
        {
            return GetTemplateItemFromCreature(creature) == NWScript.OBJECT_INVALID;
        }

        private static uint GetTemplateItemFromCreature(uint creature)
        {
            return NWScript.GetItemPossessedBy(creature, TemplateItemResRef);
        }

        public static void SetStrMod(uint creature, int value)
        {
            SetLocalInt(StrModVar, creature, value);
        }

        public static void SetDexMod(uint creature, int value)
        {
            SetLocalInt(DexModVar, creature, value);
        }

        public static void SetConMod(uint creature, int value)
        {
            SetLocalInt(ConModVar, creature, value);
        }

        public static void SetIntMod(uint creature, int value)
        {
            SetLocalInt(IntModVar, creature, value);
        }

        public static void SetWisMod(uint creature, int value)
        {
            SetLocalInt(WisModVar, creature, value);
        }

        public static void SetChaMod(uint creature, int value)
        {
            SetLocalInt(ChaModVar, creature, value);
        }

        public static void SetSubRace(uint creature, string value)
        {
            SetLocalString(SubRaceVar, creature, value);
        }

        private static void SetLocalInt(string lvar, uint creature, int value)
        {
            NWScript.SetLocalInt(GetTemplateItemFromCreature(creature), lvar, value);
        }

        private static void SetLocalString(string lvar, uint creature, string value)
        {
            NWScript.SetLocalString(GetTemplateItemFromCreature(creature), lvar, value);
        }

        public static bool Initialized(in uint nwnObjectId)
        {
            return NWScript.GetLocalInt(GetTemplateItemFromCreature(nwnObjectId), InitializedVar) == NWScript.TRUE;
        }
    }
}