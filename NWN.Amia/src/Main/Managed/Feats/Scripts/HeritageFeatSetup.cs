using System;
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
        private const string HeritageSetupVar = "heritage_setup";

        private static uint _nwnObject;
        private static int _playerRace;
        private static Player _player;
        private static uint _pckey;

        public int Run(uint nwnObjectId)
        {
            Console.WriteLine("TEST");
            _nwnObject = nwnObjectId;
            _player = new Player(_nwnObject);
            _pckey = NWScript.GetItemPossessedBy(_nwnObject, "ds_pckey");
            _playerRace = ResolvePlayerRace();

            Console.WriteLine($"Player race is {_playerRace}.");
            if(HasHeritageFeat()) Console.WriteLine("Has heritage feat.");
            if(PlayerRaceIsSupported()) Console.WriteLine("Player race is supported.");
            if(HeritageFeatInitialized()) Console.WriteLine("Feat already initialized.");
            
            if (!PlayerRaceIsSupported() || HeritageFeatInitialized() || !HasHeritageFeat()) return 0;

            Console.WriteLine("------------> Performing initial heritage feat setup.");
            
            PerformHeritageFeatSetup();
            FlagHeritageAsSetup();

            return 0;
        }

        private static bool HasHeritageFeat() => NWScript.GetHasFeat(1238, _nwnObject) == NWScript.TRUE;

        private static int ResolvePlayerRace() =>
            _player.Subrace.ToLower() switch
            {
                "aasimar" => (int) ManagedRaces.RacialType.Aasimar,
                "tiefling" => (int) ManagedRaces.RacialType.Tiefling,
                "feytouched" => (int) ManagedRaces.RacialType.Feytouched,
                "feyri" => (int) ManagedRaces.RacialType.Feyri,
                _ => _playerRace
            };

        private static bool PlayerRaceIsSupported() => ManagedRaces.HeritageRaces.ContainsKey(_playerRace);

        private static bool HeritageFeatInitialized() =>
            NWScript.GetLocalInt(_pckey, HeritageSetupVar) == NWScript.TRUE;

        private static void FlagHeritageAsSetup() => NWScript.SetLocalInt(_pckey, HeritageSetupVar, NWScript.TRUE);


        private static void PerformHeritageFeatSetup() => ManagedRaces.HeritageRaces[_playerRace].SetupStats(_player);
    }
}