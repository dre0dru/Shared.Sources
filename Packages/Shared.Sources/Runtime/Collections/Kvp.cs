using System;
using UnityEngine;

namespace Shared.Sources.Collections
{
    [Serializable]
    public struct Kvp<TKey, TValue>
    {
        [SerializeField]
        public TKey Key;

        [SerializeField]
        public TValue Value;
    }
}
