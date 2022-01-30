using System;
using System.Collections;
using System.Collections.Generic;
using Shared.Sources.Collections;
using UnityEngine;

namespace Shared.Sources.Stats
{
    [Serializable]
    public class StatsCollection<TKey, TValue> : Stat<TKey, Stat<TKey, TValue>>
    {
      
    }
}
