using UnityEngine;

namespace Shared.Sources.Editor.RuntimeAssets
{
    public abstract class AssetsScanner : ScriptableObject
    {
        #if EASY_BUTTONS
        [EasyButtons.Button]
        #endif
        public abstract void Scan();
    }
}
