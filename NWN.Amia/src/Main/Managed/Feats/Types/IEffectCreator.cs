using System.Collections.Generic;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats.Types
{
    public interface IEffectCreator
    {
        List<Effect> GetEffects(uint objectId);
    }
}