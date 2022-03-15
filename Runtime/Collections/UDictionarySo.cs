using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.Collections
{
    public class UDictionarySo<TKey, TValue> : DictionarySo<TKey, TValue>
    {
        [SerializeField]
        private UDictionary<TKey, TValue> _dictionary;

        public override TValue this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        public override Dictionary<TKey, TValue>.KeyCollection Keys => _dictionary.Keys;
        public override Dictionary<TKey, TValue>.ValueCollection Values => _dictionary.Values;

        public override int Count => _dictionary.Count;

        public override bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).IsReadOnly;

        public override void Add(TKey key, TValue value) =>
            _dictionary.Add(key, value);

        public override bool Remove(TKey key) =>
            _dictionary.Remove(key);

        public override bool ContainsKey(TKey key) =>
            _dictionary.ContainsKey(key);

        public override bool TryGetValue(TKey key, out TValue value) =>
            _dictionary.TryGetValue(key, out value);

        public override void Clear() =>
            _dictionary.Clear();

        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            _dictionary.GetEnumerator();

        public override void Add(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Add(item);

        public override bool Remove(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Remove(item);

        public override bool Contains(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(item);

        public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
    }
}
