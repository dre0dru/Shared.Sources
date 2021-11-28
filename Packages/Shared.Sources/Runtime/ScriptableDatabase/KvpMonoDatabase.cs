using System.Collections.Generic;
using System.Linq;
using Shared.Sources.Collections;
using UnityEngine;

namespace Shared.Sources.ScriptableDatabase
{
    //TODO maybe create instance class and pass there instance on Awake?
    public class KvpMonoDatabase<TKey, TValue> : MonoDatabase<TKey, TValue>
    {
        [SerializeField]
        protected List<Kvp<TKey, TValue>> _keysToValues;

        private Dictionary<TKey, TValue> _keysMap;

        public override IEnumerable<TKey> Keys => _keysMap.Keys;

        private void Awake() =>
            _keysMap = _keysToValues.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        public override TValue Get(TKey key) =>
            _keysMap[key];

        public override bool TryGet(TKey key, out TValue value) =>
            _keysMap.TryGetValue(key, out value);

        public override bool ContainsKey(TKey key) =>
            _keysMap.ContainsKey(key);

        #if UNITY_EDITOR
        public override void AddOrUpdate(TKey key, TValue value)
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

        public override bool Remove(TKey key)
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
