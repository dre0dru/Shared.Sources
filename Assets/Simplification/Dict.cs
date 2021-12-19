using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Shared.Sources.Collections;
using UnityEngine;

namespace Simplification
{
    [Serializable]
    public struct KvpNew<TKey, TValue> : IKvp<TKey, TValue>
    {
        [SerializeField]
        private TKey _key;

        [SerializeField]
        private TValue _value;

        public TKey Key
        {
            get => _key;
            set => _key = value;
        }

        public TValue Value
        {
            get => _value;
            set => _value = value;
        }
    }
    
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class Dict<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<KvpNew<TKey, TValue>> _serialized = new List<KvpNew<TKey, TValue>>();

        [SerializeField, HideInInspector]
        private bool _hasCollisions;

        private Dictionary<TKey, TValue> _runtimeDictionary;

        private Dictionary<TKey, TValue> Dictionary
        {
            get
            {
                _runtimeDictionary ??= _serialized.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                #if !UNITY_EDITOR
                if(_serialized != null)
                {
                    //Clearing serialized list so no data is duplicated in memory
                    _serialized.Clear();
                    _serialized = null;
                }
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
        public int Count => Dictionary.Count;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public Dict()
        {
        }

        public Dict(IDictionary<TKey, TValue> dictionary)
        {
            foreach (var pair in dictionary)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);

            #if UNITY_EDITOR
            AddOrUpdateList(key, value);
            #endif
        }

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

        public bool ContainsKey(TKey key) => Dictionary.ContainsKey(key);

        public bool TryGetValue(TKey key, out TValue value) => Dictionary.TryGetValue(key, out value);

        public void Clear()
        {
            Dictionary.Clear();
            #if UNITY_EDITOR
            _serialized.Clear();
            #endif
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).IsReadOnly;

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Add(item);

            #if UNITY_EDITOR
            AddOrUpdateList(item.Key, item.Value);
            #endif
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item);

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex);

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) =>
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Remove(item);

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            ((IEnumerable<KeyValuePair<TKey, TValue>>)Dictionary).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Dictionary).GetEnumerator();

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
                _serialized[index] = new KvpNew<TKey, TValue>()
                {
                    Key = key,
                    Value = value
                };
            }
            else
            {
                _serialized.Add( new KvpNew<TKey, TValue>
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
}
