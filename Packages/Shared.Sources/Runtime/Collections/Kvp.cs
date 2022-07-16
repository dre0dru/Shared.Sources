using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.Collections
{
    public interface IKvp<TKey, TValue>
    {
        TKey Key { get; set; }
        TValue Value { get; set; }
    }

    [Serializable]
    public struct Kvp<TKey, TValue> : IKvp<TKey, TValue>, IEquatable<Kvp<TKey, TValue>>
    {
        [SerializeField]
        private TKey _key;

        [SerializeField]
        private TValue _value;

        public TKey Key
        {
            get => _key;
            set => _key = value;
        }

        public TValue Value
        {
            get => _value;
            set => _value = value;
        }

        public bool Equals(Kvp<TKey, TValue> other) =>
            EqualityComparer<TKey>.Default.Equals(_key, other._key) &&
            EqualityComparer<TValue>.Default.Equals(_value, other._value);

        public override bool Equals(object obj) =>
            obj is Kvp<TKey, TValue> other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TKey>.Default.GetHashCode(_key) * 397) ^ EqualityComparer<TValue>.Default.GetHashCode(_value);
            }
        }

        public static implicit operator KeyValuePair<TKey, TValue>(Kvp<TKey, TValue> kvp) =>
            new KeyValuePair<TKey, TValue>(kvp._key, kvp._value);

        public static implicit operator Kvp<TKey, TValue>(KeyValuePair<TKey, TValue> kvp) =>
            new Kvp<TKey, TValue>
            {
                _key = kvp.Key,
                _value = kvp.Value
            };
    }
}
