using System;

namespace Shared.Sources.Stats
{
    [Serializable]
    public class StatsCollection<TKey, TValue> : Stat<TKey, Stat<TKey, TValue>>
    {
      
    }
}
