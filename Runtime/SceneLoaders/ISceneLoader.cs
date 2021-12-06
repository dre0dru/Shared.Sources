#if UNITASK_SUPPORT

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
#if VCONTAINER_SUPPORT
using VContainer;
#endif

namespace Shared.Sources.SceneLoaders
{
    public interface ISceneLoader
    {
        int LoadedScenesCount { get; }

        IEnumerable<string> LoadedScenesNames { get; }

        void SetSceneActive(string sceneName);

        UniTask LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode, IProgress<float> progress = null,
            bool makeActive = false);

        #if VCONTAINER_SUPPORT
        UniTask LoadSceneAsyncWithExtraBindings(string sceneName, LoadSceneMode loadSceneMode,
            Action<IContainerBuilder> extraBindings,
            IProgress<float> progress = null, bool makeActive = false);
        #endif

        UniTask UnloadSceneAsync(string sceneName, UnloadSceneOptions unloadSceneOptions = UnloadSceneOptions.None,
            IProgress<float> progress = null);
    }
}

#endif
