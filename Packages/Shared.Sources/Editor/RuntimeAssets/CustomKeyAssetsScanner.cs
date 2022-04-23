using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor.RuntimeAssets
{
    public abstract class CustomKeyAssetsScanner<TKey, TRuntimeAsset> : AssetsScanner
        where TRuntimeAsset : Object
    {
        [SerializeField]
        protected List<ScannerTarget<TKey, TRuntimeAsset>> _scannerTargets;

        public override void Scan()
        {
            foreach (var scannerTarget in _scannerTargets)
            {
                var folderPaths = scannerTarget.ScanFolders.GetFoldersPaths();

                var assets = AssetDatabaseUtils.LoadAssetsAtPaths<TRuntimeAsset>(folderPaths);

                scannerTarget.Target.Clear();

                foreach (var runtimeAsset in assets)
                {
                    scannerTarget.Target.Add(GetKeyFromAsset(runtimeAsset), runtimeAsset);
                }

                AssetDatabaseUtils.SetDirtyAndSave(scannerTarget.Target);
            }

            AssetDatabase.Refresh();
        }

        public abstract TKey GetKeyFromAsset(TRuntimeAsset runtimeAsset);
    }
}
