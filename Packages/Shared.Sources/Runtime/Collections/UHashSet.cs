using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using UnityEngine;

namespace Shared.Sources.Collections
{
    //TODO UHashSetMono, UHashSetSo
    [Serializable]
    [DebuggerDisplay("Count = {Count}")]
    public class UHashSet<TValue> : ISet<TValue>, ISerializationCallbackReceiver,
        IDeserializationCallback, ISerializable
    {
        protected class SerializableHashSet : HashSet<TValue>
        {
            public SerializableHashSet()
            {
            }

            public SerializableHashSet(IEnumerable<TValue> values) : base(values)
            {
            }

            public SerializableHashSet(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        [SerializeField]
        private List<TValue> _serialized;

        private SerializableHashSet _runtimeHashSet;

        private SerializableHashSet HashSet => _runtimeHashSet;

        public int Count => HashSet.Count;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public UHashSet()
        {
            _runtimeHashSet = new SerializableHashSet();
            
            #if UNITY_EDITOR
            _serialized = new List<TValue>();
            #endif
        }

        public UHashSet(IEnumerable<TValue> values)
        {
            _runtimeHashSet = new SerializableHashSet(values);
            
            #if UNITY_EDITOR
            _serialized = new List<TValue>(_runtimeHashSet);
            #endif
        }
        
        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public UHashSet(SerializationInfo info, StreamingContext context)
        {
            _runtimeHashSet = new SerializableHashSet(info, context);
            
            #if UNITY_EDITOR
            _serialized = new List<TValue>(_runtimeHashSet);
            #endif
        }

        public bool Add(TValue item)
        {
            #if UNITY_EDITOR
            AddToListIfNotPresent(item);
            #endif
            
            return HashSet.Add(item);
        }

        public bool Remove(TValue item)
        {
            #if UNITY_EDITOR
            _serialized.Remove(item);
            #endif

            return HashSet.Remove(item);
        }

        public bool Contains(TValue item) =>
            HashSet.Contains(item);

        public void Clear() =>
            HashSet.Clear();
        
        public void ExceptWith(IEnumerable<TValue> other) =>
            HashSet.ExceptWith(other);

        public void SymmetricExceptWith(IEnumerable<TValue> other) =>
            HashSet.SymmetricExceptWith(other);

        public void IntersectWith(IEnumerable<TValue> other) =>
            HashSet.IntersectWith(other);

        public void UnionWith(IEnumerable<TValue> other) =>
            HashSet.UnionWith(other);

        public bool IsProperSubsetOf(IEnumerable<TValue> other) =>
            HashSet.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<TValue> other) =>
            HashSet.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<TValue> other) =>
            HashSet.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<TValue> other) =>
            HashSet.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<TValue> other) =>
            HashSet.Overlaps(other);

        public bool SetEquals(IEnumerable<TValue> other) =>
            HashSet.SetEquals(other);

        bool ICollection<TValue>.IsReadOnly =>
            ((ICollection<TValue>)HashSet).IsReadOnly;
        
        void ICollection<TValue>.Add(TValue item)
        {
            #if UNITY_EDITOR
            AddToListIfNotPresent(item);
            #endif
            
            ((ICollection<TValue>)HashSet).Add(item);
        }
        
        public void CopyTo(TValue[] array, int arrayIndex) =>
            ((ICollection<TValue>)HashSet).CopyTo(array, arrayIndex);

        public IEnumerator<TValue> GetEnumerator() =>
            ((IEnumerable<TValue>)HashSet).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            #if !UNITY_EDITOR
            OnBeforeSerialize();
            #endif
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            #if UNITY_EDITOR
            CheckForCollisions();
            OnAfterDeserializeEditor();
            #endif

            #if !UNITY_EDITOR
            OnAfterDeserializePlayer();
            #endif
        }

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            ((IDeserializationCallback)HashSet).OnDeserialization(sender);
            
            #if UNITY_EDITOR
            OnBeforeSerialize();
            CheckForCollisions();
            #endif
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)HashSet).GetObjectData(info, context);
        }
        
        private void OnBeforeSerialize()
        {
            _serialized = new List<TValue>(HashSet);
        }
        
        #if !UNITY_EDITOR
        private void OnAfterDeserializePlayer()
        {
            HashSet.Clear();
            foreach (var value in _serialized)
            {
                HashSet.Add(value);
            }

            _serialized = null;
        }
        #endif
        
        #if UNITY_EDITOR
        #pragma warning disable CS0414
        private bool _hasCollisions;
        #pragma warning restore CS0414
        
        private void CheckForCollisions()
        {
            var hashSet = new HashSet<TValue>();

            foreach (var value in _serialized)
            {
                if (!hashSet.Add(value))
                {
                    _hasCollisions = true;
                    return;
                }
            }

            _hasCollisions = false;
        }

        private void OnAfterDeserializeEditor()
        {
            HashSet.Clear();
            foreach (var value in _serialized)
            {
                HashSet.Add(value);
            }
        }

        private void AddToListIfNotPresent(TValue value)
        {
            if (_serialized.Contains(value))
            {
                return;
            }
            
            _serialized.Add(value);
        }
        #endif
    }
}
