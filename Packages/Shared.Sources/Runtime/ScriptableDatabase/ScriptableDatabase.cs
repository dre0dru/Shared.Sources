using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.ScriptableDatabase
{
    public abstract class ScriptableDatabase<TKey, TValue> : ScriptableObject,
        IScriptableDatabase<TKey, TValue>
    {
        public abstract IEnumerable<TKey> Keys { get; }

        public abstract TValue Get(TKey key);

        public abstract bool TryGet(TKey key, out TValue value);

        public abstract bool ContainsKey(TKey key);
    }

    public class ScriptableDatabaseComposite<TKey, TValue> : ScriptableDatabase<TKey, TValue>
    {
        [SerializeField]
        protected List<ScriptableDatabase<TKey, TValue>> _scriptableDatabases;

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
            }

            return _keyToDatabaseMap;
        }

        #if UNITY_EDITOR
        public void AddDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Add(scriptableDatabase);

        public bool RemoveDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Remove(scriptableDatabase);
        #endif
    }
}
