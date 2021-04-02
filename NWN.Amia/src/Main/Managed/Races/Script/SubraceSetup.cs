using JetBrains.Annotations;
using NWN.Amia.Main.Core.Types;
using NWN.Amia.Main.Managed.Races.Script.SubraceTemplates;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Script
{
    [ScriptName("subrace_setup")]
    [UsedImplicitly]
    public class SubraceSetup : IRunnableScript
    {
        public int Run(uint nwnObjectId)
        {
            return NWScript.GetSubRace(nwnObjectId).ToLower() switch
            {
                "aasimar" => new AasimarOption().Run(nwnObjectId),
                "tiefling" => new TieflingOption().Run(nwnObjectId),
                "earth genasi" => new EarthGenasiOption().Run(nwnObjectId),
                "earth" => new EarthGenasiOption().Run(nwnObjectId),
                "water genasi" => new WaterGenasiOption().Run(nwnObjectId),
                "water" => new WaterGenasiOption().Run(nwnObjectId),
                "fire genasi" => new FireGenasiOption().Run(nwnObjectId),
                "fire" => new FireGenasiOption().Run(nwnObjectId),
                "air genasi" => new AirGenasiOption().Run(nwnObjectId),
                "air" => new AirGenasiOption().Run(nwnObjectId),
                "feytouched" => new FeytouchedOption().Run(nwnObjectId),
                "aquatic elf" => new AquaticElfOption().Run(nwnObjectId),
                "feyri" => new FeyriOption().Run(nwnObjectId),
                "fey'ri" => new FeyriOption().Run(nwnObjectId),
                "shadovar" => new ShadovarOption().Run(nwnObjectId),
                "centaur" => new CentaurOption().Run(nwnObjectId),
                "avariel" => new AvarielOption().Run(nwnObjectId),
                "kenku" => new KenkuOption().Run(nwnObjectId),
                "lizardfolk" => new LizardfolkOption().Run(nwnObjectId),
                "half dragon" => new HalfDragonOption().Run(nwnObjectId),
                "half-dragon" => new HalfDragonOption().Run(nwnObjectId),

                _ => 1
            };
        }
    }
}