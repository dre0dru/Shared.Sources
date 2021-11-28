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

        #if UNITY_EDITOR
        public abstract void AddOrUpdate(TKey key, TValue value);

        public abstract bool Remove(TKey key);
        #endif
    }

    public class MonoDatabaseComposite<TKey, TValue> : MonoBehaviour,
        IScriptableDatabase<TKey, TValue>
    {
        [SerializeField]
        protected List<ScriptableDatabase<TKey, TValue>> _scriptableDatabases;

        [SerializeField]
        protected List<MonoDatabase<TKey, TValue>> _monoDatabases;

        private KeyToDatabaseMap<TKey, TValue> _keyToDatabaseMap;

        public IEnumerable<TKey> Keys => _keyToDatabaseMap.Keys;

        private void Awake()
        {
            _keyToDatabaseMap = new KeyToDatabaseMap<TKey, TValue>();
            
            _keyToDatabaseMap.FillFromDatabases(_scriptableDatabases);
            _keyToDatabaseMap.FillFromDatabases(_monoDatabases);
        }

        public TValue Get(TKey key) =>
            _keyToDatabaseMap[key].Get(key);

        public bool TryGet(TKey key, out TValue value) =>
            _keyToDatabaseMap[key].TryGet(key, out value);

        public bool ContainsKey(TKey key) =>
            _keyToDatabaseMap[key].ContainsKey(key);

        #if UNITY_EDITOR
        public void AddDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Add(scriptableDatabase);

        public bool RemoveDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Remove(scriptableDatabase);
        #endif
    }
}
