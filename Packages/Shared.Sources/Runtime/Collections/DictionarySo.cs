using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.Collections
{
    public abstract class DictionarySo<TKey, TValue> : ScriptableObject, IDictionary<TKey, TValue>
    {
        public abstract TValue this[TKey key] { get; set; }
        
        public abstract Dictionary<TKey, TValue>.KeyCollection Keys { get; }
        public abstract Dictionary<TKey, TValue>.ValueCollection Values { get; }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys;
        ICollection<TValue> IDictionary<TKey, TValue>.Values => Values;

        public abstract int Count { get; }
        public abstract bool IsReadOnly { get; }

        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);
        public abstract bool ContainsKey(TKey key);
        public abstract bool TryGetValue(TKey key, out TValue value);
        public abstract void Clear();

        public abstract IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        public abstract void Add(KeyValuePair<TKey, TValue> item);

        public abstract bool Contains(KeyValuePair<TKey, TValue> item);

        public abstract void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex);

        public abstract bool Remove(KeyValuePair<TKey, TValue> item);
    }
}

