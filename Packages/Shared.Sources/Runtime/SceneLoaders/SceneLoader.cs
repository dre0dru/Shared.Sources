#if UNITASK_SUPPORT

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
#if VCONTAINER_SUPPORT
using VContainer;
using VContainer.Unity;
#endif

namespace Shared.Sources.SceneLoaders
{
    public class SceneLoader : ISceneLoader
    {
        public int LoadedScenesCount => SceneManager.sceneCount;

        public IEnumerable<string> LoadedScenesNames
        {
            get
            {
                var loadedScenesCount = LoadedScenesCount;
                var scenesNames = new string[loadedScenesCount];

                for (int i = 0; i < loadedScenesCount; i++)
                {
                    scenesNames[i] = SceneManager.GetSceneAt(i).name;
                }

                return scenesNames;
            }
        }

        public void SetSceneActive(string sceneName)
        {
            var scene = SceneManager.GetSceneByName(sceneName);
            SceneManager.SetActiveScene(scene);
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode,
            IProgress<float> progress = null, bool makeActive = false)
        {
            await SceneManager.LoadSceneAsync(sceneName, loadSceneMode).ToUniTask(progress);

            if (makeActive)
            {
                SetSceneActive(sceneName);
            }
        }

        #if VCONTAINER_SUPPORT
        public async UniTask LoadSceneAsyncWithExtraBindings(string sceneName, LoadSceneMode loadSceneMode,
            Action<IContainerBuilder> extraBindings,
            IProgress<float> progress = null, bool makeActive = false)
        {
            using (LifetimeScope.Enqueue(extraBindings))
            {
                await LoadSceneAsync(sceneName, loadSceneMode, progress, makeActive);
            }
        }
        #endif

        public UniTask UnloadSceneAsync(string sceneName, IProgress<float> progress = null)
        {
            return SceneManager.UnloadSceneAsync(sceneName).ToUniTask(progress);
        }
    }
}

#endif
