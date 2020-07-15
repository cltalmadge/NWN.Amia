using System.Collections.Generic;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats.Types
{
    public interface IFeatEffectCreator
    {
        List<Effect> GetFeatEffects(uint objectId);
    }
}