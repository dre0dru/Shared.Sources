using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Sources.Collections
{
    //TODO CircularList decorator with with full serialization support?
    [Serializable]
    public struct ListCircularVisitor<T>
    {
        private readonly IList<T> _list;
        [SerializeField]
        private int _index;
        
        public ListCircularVisitor(IList<T> list)
        {
            _list = list;
            _index = -1;
        }

        public bool TryMoveNext(out T next) =>
            _list.TryMoveNextCircular(ref _index, out next);

        public bool TryMovePrev(out T next) =>
            _list.TryMovePrevCircular(ref _index, out next);
    }
}
