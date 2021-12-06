using System.Collections.Generic;

namespace Shared.Sources.Collections
{
    public class SentinelList<T>
    {
        private class Node
        {
            public T Value;
            public Node Next;
            public Node Prev;
        }

        private readonly IEqualityComparer<T> _equalityComparer;
        private readonly Node _sentinel;

        public T SentinelNext => _sentinel.Next.Value;
        public T SentinelPrev => _sentinel.Prev.Value;

        public SentinelList(T sentinelValue) : this(sentinelValue, EqualityComparer<T>.Default)
        {
        }

        public SentinelList(T sentinelValue, IEqualityComparer<T> equalityComparer)
        {
            _equalityComparer = equalityComparer;
            _sentinel = new Node()
            {
                Value = sentinelValue,
            };
            _sentinel.Next = _sentinel;
            _sentinel.Prev = _sentinel;
        }

        public void AddNode(T value)
        {
            var node = new Node()
            {
                Value = value
            };

            node.Next = _sentinel;
            node.Prev = _sentinel.Prev;
            node.Next.Prev = node;
            node.Prev.Next = node;
        }

        public void AddNode(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                AddNode(value);
            }
        }
        
        public bool RemoveNode(T value)
        {
            var node = _sentinel.Next;

            while (node != _sentinel)
            {
                if (_equalityComparer.Equals(node.Value, value))
                {
                    node.Prev.Next = node.Next;
                    node.Next.Prev = node.Prev;
                    return true;
                }
                
                node = node.Next;
            }

            return false;
        }
        
        public bool RemoveNode(IEnumerable<T> values)
        {
            var isRemoved = false;
            
            foreach (var value in values)
            {
                isRemoved |= RemoveNode(value);
            }

            return isRemoved;
        }

        public void AdvanceSentinel()
        {
            var prev = _sentinel.Prev;
            var next = _sentinel.Next;

            if (prev == next)
            {
                return;
            }
            
            var nextNext = next.Next;

            next.Prev = prev;
            prev.Next = next;
            
            _sentinel.Prev = next;
            _sentinel.Next = nextNext;
            
            next.Next = _sentinel;
            nextNext.Prev = _sentinel;
        }
    }
}