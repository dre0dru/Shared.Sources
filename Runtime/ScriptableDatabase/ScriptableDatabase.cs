using System.Collections.Generic;
using System.Linq;
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

        private void Awake()
        {
            _keyToDatabaseMap = new KeyToDatabaseMap<TKey, TValue>();
            
            if (_scriptableDatabases != null)
            {
                _keyToDatabaseMap.FillFromDatabases(_scriptableDatabases);
            }
        }

        public override IEnumerable<TKey> Keys => _keyToDatabaseMap?.Keys ?? Enumerable.Empty<TKey>();

        public override TValue Get(TKey key) =>
            _keyToDatabaseMap[key].Get(key);

        public override bool TryGet(TKey key, out TValue value) =>
            _keyToDatabaseMap[key].TryGet(key, out value);

        public override bool ContainsKey(TKey key) =>
            _keyToDatabaseMap[key].ContainsKey(key);

        #if UNITY_EDITOR
        public void AddDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Add(scriptableDatabase);

        public bool RemoveDatabase(ScriptableDatabase<TKey, TValue> scriptableDatabase) =>
            _scriptableDatabases.Remove(scriptableDatabase);
        #endif
    }
}
