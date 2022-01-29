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

        public Stat<TKey, TValue> Add(TKey key, TValue value)
        {
            _values[key] = value;

            return this;
        }

        public bool Has(TKey key) =>
            _values.ContainsKey(key);

        public bool Remove(TKey key) =>
            _values.Remove(key);

        public Stat<TKey, TValue> OverrideFrom(Stat<TKey, TValue> stat)
        {
            foreach (var kvp in stat)
            {
                this[kvp.Key] = kvp.Value;
            }

            return this;
        }

        public Stat<TKey, TValue> Override(Stat<TKey, TValue> stat)
        {
            foreach (var kvp in this)
            {
                stat[kvp.Key] = kvp.Value;
            }

            return stat;
        }

        public Stat<TKey, TValue> Clone()
        {
            var stat = new Stat<TKey, TValue>();

            return Override(stat);
        }
        
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
