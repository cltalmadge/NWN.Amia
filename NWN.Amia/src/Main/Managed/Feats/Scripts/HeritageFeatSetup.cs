using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Characters;
using NWN.Amia.Main.Managed.Races;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats.Scripts
{
    [ScriptName("heritage_setup"), UsedImplicitly]
    public class HeritageFeatSetup : IRunnableScript
    {
        private static uint _nwnObject;
        private static readonly int PlayerRace = NWScript.GetRacialType(_nwnObject);
        private static Player _player;
        private static uint _pckey;

        public int Run(uint nwnObjectId)
        {
            _nwnObject = nwnObjectId;
            _player = new Player(_nwnObject);
            _pckey = NWScript.GetItemPossessedBy(_nwnObject, "ds_pckey");


            if (PlayerRaceIsSupported() && HeritageFeatNotInitialized())
            {
                PerformHeritageFeatSetup();
            }

            return 0;
        }

        private static bool PlayerRaceIsSupported() => ManagedRaces.HeritageRaces.ContainsKey(PlayerRace);

        private static bool HeritageFeatNotInitialized() =>
            NWScript.GetLocalInt(_pckey, "heritage_setup") == NWScript.FALSE;

        private static void PerformHeritageFeatSetup() => ManagedRaces.HeritageRaces[PlayerRace].SetupStats(_player);
    }
}