using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.UDatabase
{
    public abstract class UDatabaseSo<TKey, TValue> : ScriptableObject,
        IUDatabase<TKey, TValue>
    {
        public abstract TValue this[TKey key] { get; set; }
        public abstract ICollection<TKey> Keys { get; }
        public abstract ICollection<TValue> Values { get; }

        public abstract int Count { get; }
        public abstract bool IsReadOnly { get; }

        public abstract void Add(TKey key, TValue value);
        public abstract bool ContainsKey(TKey key);
        public abstract bool Remove(TKey key);
        public abstract bool TryGetValue(TKey key, out TValue value);
        public abstract void Clear();

        public abstract IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> pair) =>
            Add(pair.Key, pair.Value);

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> pair)
        {
            if (TryGetValue(pair.Key, out var value))
            {
                return EqualityComparer<TValue>.Default.Equals(value, pair.Value);
            }

            return false;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentException("The array cannot be null");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex),
                    "The starting array index cannot be negative");
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("The destination array has fewer elements than the collection");
            
            foreach (var key in Keys)
            {
                array[arrayIndex] = new KeyValuePair<TKey, TValue>(key, this[key]);
                arrayIndex++;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> pair)
        {
            if (TryGetValue(pair.Key, out var value))
            {
                bool valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
                if (valueMatch)
                {
                    return Remove(pair.Key);
                }
            }

            return false;
        }
    }
}

