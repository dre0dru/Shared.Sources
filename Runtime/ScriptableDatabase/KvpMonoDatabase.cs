using System.Collections.Generic;
using System.Linq;
using Shared.Sources.Collections;
using UnityEngine;

namespace Shared.Sources.ScriptableDatabase
{
    public class KvpMonoDatabase<TKey, TValue> : MonoDatabase<TKey, TValue>
    {
        [SerializeField]
        protected List<Kvp<TKey, TValue>> _keysToValues;

        private Dictionary<TKey, TValue> _keysMap;

        public override IEnumerable<TKey> Keys => KeysMap.Keys;

        private Dictionary<TKey, TValue> KeysMap => GetOrCreateKeysMap();

        public override TValue Get(TKey key) =>
            KeysMap[key];

        public override bool TryGet(TKey key, out TValue value) =>
            KeysMap.TryGetValue(key, out value);

        public override bool ContainsKey(TKey key) =>
            KeysMap.ContainsKey(key);

        private Dictionary<TKey, TValue> GetOrCreateKeysMap()
        {
            if (_keysMap == null)
            {
                _keysMap = _keysToValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }

            return _keysMap;
        }

        #if UNITY_EDITOR
        public void AddOrUpdate(TKey key, TValue value)
        {
            var idx = _keysToValues.FindIndex(kvp => kvp.Key.Equals(key));

            if (idx != -1)
            {
                _keysToValues.RemoveAt(idx);
            }
            else
            {
                idx = _keysToValues.Count - 1;
            }

            _keysToValues.Insert(idx, new Kvp<TKey, TValue>()
            {
                Key = key,
                Value = value
            });
        }

        public bool Remove(TKey key)
        {
            var idx = _keysToValues.FindIndex(kvp => kvp.Key.Equals(key));

            if (idx != -1)
            {
                _keysToValues.RemoveAt(idx);
                return true;
            }

            return false;
        }
        #endif
    }
}
