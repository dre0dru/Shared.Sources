using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace Shared.Sources.Collections
{
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class UDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<Kvp<TKey, TValue>> _serialized = new List<Kvp<TKey, TValue>>();
        
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

        public Dictionary<TKey, TValue>.KeyCollection Keys => Dictionary.Keys;
        public Dictionary<TKey, TValue>.ValueCollection Values => Dictionary.Values;

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => Dictionary.Keys;
        ICollection<TValue> IDictionary<TKey, TValue>.Values => Dictionary.Values;
        
        public int Count => Dictionary.Count;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public UDictionary()
        {
            
        }
        
        public UDictionary(IDictionary<TKey, TValue> dictionary)
        {
            foreach (var pair in dictionary) {
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
                _serialized[index] = new Kvp<TKey, TValue>
                {
                    Key = key,
                    Value = value
                };
            }
            else
            {
                _serialized.Add(new Kvp<TKey, TValue>
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

        private bool CheckForCollisions()
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
