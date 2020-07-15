using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NWN.Core;

namespace NWN.Amia.Main.Managed.Feats.Types
{
    public interface IEffectCollector
    {
        List<Effect> GatherEffectsForObject(uint objectId);
    }
}