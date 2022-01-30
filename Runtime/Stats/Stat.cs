using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Shared.Sources.Collections;
using UnityEngine;

namespace Shared.Sources.Stats
{
    [Serializable]
    public class Stat<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        [SerializeField]
        private UDictionary<TKey, TValue> _values;

        public TValue this[TKey key]
        {
            get => _values[key];
            set => _values[key] = value;
        }

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public Stat()
        {
            _values = new UDictionary<TKey, TValue>();
        }

        public void Add(TKey key, TValue value) =>
            _values[key] = value;

        public TValue Get(TKey key) =>
            _values[key];

        public bool TryGet(TKey key, out TValue value) =>
            _values.TryGetValue(key, out value);

        public bool Has(TKey key) =>
            _values.ContainsKey(key);

        public bool Remove(TKey key) =>
            _values.Remove(key);

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(GetType().Name);
            sb.Append(":");
            sb.Append(Environment.NewLine);

            foreach (var kvp in _values)
            {
                sb.Append(kvp.Key);
                sb.Append("=");
                sb.Append(kvp.Value);
                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            _values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
