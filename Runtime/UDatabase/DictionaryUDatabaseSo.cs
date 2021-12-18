using System.Collections.Generic;
using Shared.Sources.Collections;
using UnityEngine;

namespace Shared.Sources.UDatabase
{
    public class DictionaryUDatabaseSo<TKey, TValue, TKvp> : UDatabaseSo<TKey, TValue>
        where TKvp : IKvp<TKey, TValue>, new()
    {
        [SerializeField]
        private UDictionary<TKey, TValue, TKvp> _database;

        public override bool TryGetValue(TKey key, out TValue value) =>
            _database.TryGetValue(key, out value);

        public override void Clear() =>
            _database.Clear();

        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            _database.GetEnumerator();

        public override TValue this[TKey key]
        {
            get => _database[key];
            set => _database[key] = value;
        }

        public override ICollection<TKey> Keys => _database.Keys;
        public override ICollection<TValue> Values => _database.Values;
        public override int Count => _database.Count;
        public override bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)_database).IsReadOnly;

        public override void Add(TKey key, TValue value) =>
            _database.Add(key, value);

        public override bool ContainsKey(TKey key) =>
            _database.ContainsKey(key);

        public override bool Remove(TKey key) =>
            _database.Remove(key);
    }
    
    public class DictionaryUDatabaseSo<TKey, TValue> : DictionaryUDatabaseSo<TKey, TValue, Kvp<TKey, TValue>>
    {
        
    }
}
