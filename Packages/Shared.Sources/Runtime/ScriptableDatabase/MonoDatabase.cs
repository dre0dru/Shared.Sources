using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.ScriptableDatabase
{
    public abstract class MonoDatabase<TKey, TValue> : MonoBehaviour,
        IScriptableDatabase<TKey, TValue>
    {
        public abstract IEnumerable<TKey> Keys { get; }

        public abstract TValue Get(TKey key);

        public abstract bool TryGet(TKey key, out TValue value);

        public abstract bool ContainsKey(TKey key);
    }

    public class MonoDatabaseComposite<TKey, TValue> : MonoDatabase<TKey, TValue>
    {
        [SerializeField]
        protected List<ScriptableDatabase<TKey, TValue>> _scriptableDatabases;

        [SerializeField]
        protected List<MonoDatabase<TKey, TValue>> _monoDatabases;

        private KeyToDatabaseMap<TKey, TValue> _keyToDatabaseMap;

        public override IEnumerable<TKey> Keys => KeyToDatabaseMap.Keys;

        private KeyToDatabaseMap<TKey, TValue> KeyToDatabaseMap => GetOrCreateKeysMap();

        public override TValue Get(TKey key) =>
            KeyToDatabaseMap[key].Get(key);

        public override bool TryGet(TKey key, out TValue value) =>
            KeyToDatabaseMap[key].TryGet(key, out value);

        public override bool ContainsKey(TKey key) =>
            KeyToDatabaseMap[key].ContainsKey(key);
        
        private KeyToDatabaseMap<TKey, TValue> GetOrCreateKeysMap()
        {
            if (_keyToDatabaseMap == null)
            {
                _keyToDatabaseMap = new KeyToDatabaseMap<TKey, TValue>();
                
                _keyToDatabaseMap.FillFromDatabases(_scriptableDatabases);
                _keyToDatabaseMap.FillFromDatabases(_monoDatabases);
            }

            return _keyToDatabaseMap;
        }

        #if UNITY_EDITOR
        public void AddDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Add(scriptableDatabase);

        public bool RemoveDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Remove(scriptableDatabase);
        
        public void AddDatabase(MonoDatabase<TKey, TValue> monoDatabase) =>
            _monoDatabases.Add(monoDatabase);

        public bool RemoveDatabase(MonoDatabase<TKey, TValue> monoDatabase) =>
            _monoDatabases.Remove(monoDatabase);
        #endif
    }
}
