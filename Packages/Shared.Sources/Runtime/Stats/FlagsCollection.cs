using System;
using System.Collections;
using System.Collections.Generic;
using Shared.Sources.Collections;
using UnityEngine;

namespace Shared.Sources.Stats
{
    [Serializable]
    public class FlagsCollection<TKey> : IEnumerable<KeyValuePair<TKey, Flag>>
    {
        [SerializeField]
        private UDictionary<TKey, Flag> _stats;

        public Flag this[TKey key] => Get(key);
        
        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public FlagsCollection() =>
            _stats = new UDictionary<TKey, Flag>();

        public FlagsCollection<TKey> Add(TKey key)
        {
            _stats[key] = new Flag();

            return this;
        }

        public FlagsCollection<TKey> Add(TKey key, Flag flag)
        {
            _stats[key] = flag;

            return this;
        }

        public Flag Get(TKey key) =>
            _stats[key];

        public bool Has(TKey key) =>
            _stats.ContainsKey(key);

        public bool TryGet(TKey key, out Flag flag) =>
            _stats.TryGetValue(key, out flag);

        public void OverrideFrom(FlagsCollection<TKey> flagsCollection)
        {
            foreach (var kvp in flagsCollection)
            {
                this[kvp.Key].OverrideFrom(kvp.Value);
            }
        }

        public void Override(FlagsCollection<TKey> flagsCollection)
        {
            foreach (var kvp in this)
            {
                flagsCollection[kvp.Key].Override(kvp.Value);
            }
        }

        public FlagsCollection<TKey> Clone()
        {
            var flagsCollection = new FlagsCollection<TKey>();

            Override(flagsCollection);

            return flagsCollection;
        }
        
        public IEnumerator<KeyValuePair<TKey, Flag>> GetEnumerator() =>
            _stats.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
