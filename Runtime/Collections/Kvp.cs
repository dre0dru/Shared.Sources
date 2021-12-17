using System;
using UnityEngine;

namespace Shared.Sources.Collections
{
    public interface IKvp<TKey, TValue>
    {
        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
    
    [Serializable]
    public struct Kvp<TKey, TValue> : IKvp<TKey, TValue>
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
    }
}
