using UnityEngine;

namespace Shared.Sources.CustomTypes
{
    public class DataAsset<TData> : ScriptableObject
    {
        [SerializeField]
        protected TData _data;

        public TData Data
        {
            get => _data;
            protected set => _data = value;
        }
    }
}
