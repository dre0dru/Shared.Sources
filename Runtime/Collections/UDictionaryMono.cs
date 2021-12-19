using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.Collections
{
   public class UDictionaryMono<TKey, TValue, TKvp> : DictionaryMono<TKey, TValue>
        where TKvp : IKvp<TKey, TValue>, new()
    {
        [SerializeField]
        private UDictionary<TKey, TValue, TKvp> _dictionary;

        public override TValue this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }

        public override ICollection<TKey> Keys => _dictionary.Keys;
        public override ICollection<TValue> Values => _dictionary.Values;

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

    public class UDictionaryMono<TKey, TValue> : UDictionaryMono<TKey, TValue, Kvp<TKey, TValue>>
    {
    }
}
