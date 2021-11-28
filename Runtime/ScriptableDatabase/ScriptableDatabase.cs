using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.ScriptableDatabase
{
    //TODO это ведь по сути врраппер над dictionary, может тупо сделать внутри дикт, сделать, чтобы он в Awake создавался и все
    public abstract class ScriptableDatabase<TKey, TValue> : ScriptableObject,
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

    public class ScriptableDatabaseComposite<TKey, TValue> : ScriptableObject,
        IScriptableDatabase<TKey, TValue>
    {
        [SerializeField]
        protected List<ScriptableDatabase<TKey, TValue>> _scriptableDatabases;

        private KeyToDatabaseMap<TKey, TValue> _keyToDatabaseMap;

        private void Awake()
        {
            _keyToDatabaseMap = new KeyToDatabaseMap<TKey, TValue>();
            
            _keyToDatabaseMap.FillFromDatabases(_scriptableDatabases);
        }

        public IEnumerable<TKey> Keys => _keyToDatabaseMap.Keys;

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
