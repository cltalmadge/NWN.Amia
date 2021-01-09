using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;

using NWN.Amia.Main.Managed.Objects;
using NWN.Amia.Main.Managed.Races;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats.Scripts
{
    [ScriptName("heritage_setup")]
    [UsedImplicitly]
    public class HeritageFeatSetup : IRunnableScript
    {
        private const string HeritageSetupVar = "heritage_setup";

        private static uint _nwnObject;
        private static int _playerRace;
        private static Player _player;
        private static uint _pckey;

        public int Run(uint nwnObjectId)
        {
            _nwnObject = nwnObjectId;
            _player = new Player(_nwnObject);
            _pckey = NWScript.GetItemPossessedBy(_nwnObject, "ds_pckey");
            _playerRace = ResolvePlayerRace();

            if (!PlayerRaceIsSupported() || HeritageFeatInitialized() || !HasHeritageFeat()) return 0;

            PerformHeritageFeatSetup();
            FlagHeritageAsSetup();

            return 0;
        }

        private static bool HasHeritageFeat()
        {
            return NWScript.GetHasFeat(1238, _nwnObject) == NWScript.TRUE;
        }

        private static int ResolvePlayerRace() =>
            _player.Subrace.ToLower() switch
            {
                "aasimar" => (int) ManagedRaces.RacialType.Aasimar,
                "tiefling" => (int) ManagedRaces.RacialType.Tiefling,
                "feytouched" => (int) ManagedRaces.RacialType.Feytouched,
                "feyri" => (int) ManagedRaces.RacialType.Feyri,
                _ => NWScript.GetRacialType(_nwnObject)
            };

        private static bool PlayerRaceIsSupported() => ManagedRaces.HeritageRaces.ContainsKey(_playerRace);

        private static bool HeritageFeatInitialized() =>
            NWScript.GetLocalInt(_pckey, HeritageSetupVar) == NWScript.TRUE;

        private static void PerformHeritageFeatSetup() => ManagedRaces.HeritageRaces[_playerRace].SetupStats(_player);

        private static void FlagHeritageAsSetup() => NWScript.SetLocalInt(_pckey, HeritageSetupVar, NWScript.TRUE);
    }
}