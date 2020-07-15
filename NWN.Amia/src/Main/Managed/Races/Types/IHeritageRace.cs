using System.Collections.Generic;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Races.Types
{
    public interface IHeritageRace
    {
        void AddHeritageEffects(List<Effect> effectsToAddTo);
    }
}