using UnityEngine;

namespace Shared.Sources.Editor.RuntimeAssets
{
    public class AssetNameAsKeyAssetsScanner<TRuntimeAsset> : CustomKeyAssetsScanner<string, TRuntimeAsset>
        where TRuntimeAsset : Object
    {
        public override string GetKeyFromAsset(TRuntimeAsset runtimeAsset)
        {
            return runtimeAsset.name;
        }
    }
}
