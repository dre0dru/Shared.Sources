using System;
using System.Collections;
using System.Collections.Generic;
using Shared.Sources.Collections;
using UnityEngine;

namespace Shared.Sources.Stats
{
    [Serializable]
    public class StatsCollection<TKey, TValue> : IEnumerable<KeyValuePair<TKey, Stat<TKey, TValue>>>
    {
        [SerializeField]
        private UDictionary<TKey, Stat<TKey, TValue>> _stats;

        public Stat<TKey, TValue> this[TKey key] => Get(key);

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public StatsCollection() =>
            _stats = new UDictionary<TKey, Stat<TKey, TValue>>();

        public StatsCollection<TKey, TValue> Add(TKey key)
        {
            _stats[key] = new Stat<TKey, TValue>();

            return this;
        }

        public StatsCollection<TKey, TValue> Add(TKey key, Stat<TKey, TValue> stat)
        {
            _stats[key] = stat;

            return this;
        }

        public Stat<TKey, TValue> Get(TKey key) =>
            _stats[key];

        public bool Has(TKey key) =>
            _stats.ContainsKey(key);

        public bool TryGet(TKey key, out Stat<TKey, TValue> stat) =>
            _stats.TryGetValue(key, out stat);

        public void OverrideFrom(StatsCollection<TKey, TValue> statsCollection)
        {
            foreach (var kvp in statsCollection)
            {
                this[kvp.Key].OverrideFrom(kvp.Value);
            }
        }

        public void Override(StatsCollection<TKey, TValue> statsCollection)
        {
            foreach (var kvp in this)
            {
                statsCollection[kvp.Key].Override(kvp.Value);
            }
        }

        public StatsCollection<TKey, TValue> Clone()
        {
            var statsCollection = new StatsCollection<TKey, TValue>();

            Override(statsCollection);

            return statsCollection;
        }

        public IEnumerator<KeyValuePair<TKey, Stat<TKey, TValue>>> GetEnumerator() =>
            _stats.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}
