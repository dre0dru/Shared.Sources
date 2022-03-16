using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Shared.Sources.Editor.RuntimeAssets
{
    public class NameAsKeyAssetsScanner<TRuntimeAsset> : AssetsScanner
        where TRuntimeAsset: Object
    {
        [SerializeField]
        protected List<ScannerTarget<string, TRuntimeAsset>> _scannerTargets;

        public override void Scan()
        {
            foreach (var scannerTarget in _scannerTargets)
            {
                var folderPaths = scannerTarget.ScanFolders.GetFoldersPaths();

                var assets = AssetDatabaseUtils.LoadAssetsAtPaths<TRuntimeAsset>(folderPaths);

                scannerTarget.Target.Clear();
                
                foreach (var runtimeAsset in assets)
                {
                    scannerTarget.Target.Add(runtimeAsset.name, runtimeAsset);
                }
                
                AssetDatabaseUtils.SetDirtyAndSave(scannerTarget.Target);
            }
            
            AssetDatabase.Refresh();
        }
    }
}
