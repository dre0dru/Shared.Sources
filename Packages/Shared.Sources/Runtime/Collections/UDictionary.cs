using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Inspired by
// https://github.com/upscalebaby/generic-serializable-dictionary
// https://gist.github.com/Moe-Baker/e36610361012d586b1393994febeb5d2
namespace Shared.Sources.Collections
{
    [Serializable]
    public class UDictionary<TKey, TValue, TKvp> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
        where TKvp : IKvp<TKey, TValue>, new()
    {
        [SerializeField]
        private List<TKvp> _serialized = new List<TKvp>();

        private Dictionary<TKey, TValue> _runtimeDictionary;

        private Dictionary<TKey, TValue> Dictionary
        {
            get
            {
                _runtimeDictionary ??= _serialized.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                #if !UNITY_EDITOR
                //Clearing serialized list so no data is duplicated in memory
                _serialized.Clear();
                _serialized = null;
                #endif

                return _runtimeDictionary;
            }
        }

        public TValue this[TKey key]
        {
            get => Dictionary[key];
            set
            {
                Dictionary[key] = value;

                #if UNITY_EDITOR
                AddOrUpdateList(key, value);
                #endif
            }
        }

        public ICollection<TKey> Keys => Dictionary.Keys;
        public ICollection<TValue> Values => Dictionary.Values;

        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);

            #if UNITY_EDITOR
            _serialized.Add(new TKvp()
            {
                Key = key,
                Value = value
            });
            #endif
        }

        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

        public bool Remove(TKey key)
        {
            #if UNITY_EDITOR
            if (TryFindListIndexByKey(key, out var index))
            {
                _serialized.RemoveAt(index);
            }
            #endif

            return Dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

        public int Count => Dictionary.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).IsReadOnly;

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> pair)
        {
            Add(pair.Key, pair.Value);
        }

        public void Clear()
        {
            Dictionary.Clear();
            #if UNITY_EDITOR
            _serialized.Clear();
            #endif
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> pair)
        {
            if (Dictionary.TryGetValue(pair.Key, out var value))
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
            if (array.Length - arrayIndex < Dictionary.Count)
                throw new ArgumentException("The destination array has fewer elements than the collection");

            foreach (var pair in Dictionary)
            {
                array[arrayIndex] = pair;
                arrayIndex++;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> pair)
        {
            if (Dictionary.TryGetValue(pair.Key, out var value))
            {
                bool valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
                if (valueMatch)
                {
                    return Remove(pair.Key);
                }
            }

            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            ((IEnumerable<KeyValuePair<TKey, TValue>>)Dictionary).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Dictionary).GetEnumerator();

        [SerializeField, HideInInspector]
        private bool _hasCollisions;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            #if UNITY_EDITOR
            _hasCollisions = CheckForCollisions();
            _runtimeDictionary = null;
            #endif
        }

        #if UNITY_EDITOR
        private void AddOrUpdateList(TKey key, TValue value)
        {
            if (TryFindListIndexByKey(key, out var index))
            {
                _serialized[index] = new TKvp()
                {
                    Key = key,
                    Value = value
                };
            }
            else
            {
                _serialized.Add(new TKvp()
                {
                    Key = key,
                    Value = value
                });
            }
        }

        private bool TryFindListIndexByKey(TKey key, out int index)
        {
            index = _serialized.FindIndex(kvp => kvp.Key.Equals(key));

            return index != -1;
        }

        public bool CheckForCollisions()
        {
            var hashSet = new HashSet<TKey>();

            foreach (var kvp in _serialized)
            {
                if (!hashSet.Add(kvp.Key))
                {
                    return true;
                }
            }

            return false;
        }
        #endif
    }

    [Serializable]
    public class UDictionary<TKey, TValue> : UDictionary<TKey, TValue, Kvp<TKey, TValue>>
    {
        
    }
}
