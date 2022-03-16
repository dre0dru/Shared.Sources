using System;
using Shared.Sources.Collections;
using Shared.Sources.CustomTypes;
using UnityEngine;

namespace Shared.Sources.Editor.RuntimeAssets
{
    [Serializable]
    public struct ScannerTarget<TKey, TRuntimeAsset>
    {
        [SerializeField]
        public UDictionarySo<TKey, TRuntimeAsset> Target;

        [SerializeField]
        public FolderReference[] ScanFolders;
    }
}
