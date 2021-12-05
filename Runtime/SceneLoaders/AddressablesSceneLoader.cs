#if ADDRESSABLES_SUPPORT && UNITASK_SUPPORT

using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
#if VCONTAINER_SUPPORT
using VContainer;
using VContainer.Unity;
#endif

namespace Shared.Sources.SceneLoaders
{
    public class AddressablesSceneLoader : ISceneLoader
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly HashSet<string> _buildSettingsScenesNames;
        private readonly Dictionary<string, AsyncOperationHandle<SceneInstance>> _addressablesScenes;

        public int LoadedScenesCount => _sceneLoader.LoadedScenesCount;

        public IEnumerable<string> LoadedScenesNames => _sceneLoader.LoadedScenesNames;

        [UnityEngine.Scripting.RequiredMember]
        public AddressablesSceneLoader()
        {
            _sceneLoader = new SceneLoader();
            _buildSettingsScenesNames = GetBuildSettingsScenes();
            _addressablesScenes = new Dictionary<string, AsyncOperationHandle<SceneInstance>>();
        }

        public void SetSceneActive(string sceneName)
        {
            _sceneLoader.SetSceneActive(sceneName);
        }

        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode,
            IProgress<float> progress = null, bool makeActive = false)
        {
            if (IsBuildSettingsScene(sceneName))
            {
                await _sceneLoader.LoadSceneAsync(sceneName, loadSceneMode, progress, makeActive);
                return;
            }

            var handle = Addressables.LoadSceneAsync(sceneName, loadSceneMode);
            _addressablesScenes[sceneName] = handle;
            await handle.ToUniTask(progress);

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

        public UniTask UnloadSceneAsync(string sceneName, UnloadSceneOptions unloadSceneOptions = UnloadSceneOptions.None,
            IProgress<float> progress = null)
        {
            if (IsBuildSettingsScene(sceneName))
            {
                return _sceneLoader.UnloadSceneAsync(sceneName, unloadSceneOptions, progress);
            }

            var sceneHandle = _addressablesScenes[sceneName];

            _addressablesScenes.Remove(sceneName);

            #if ADDRESSABLES_SUPPORT_1_19
            return Addressables.UnloadSceneAsync(sceneHandle, unloadSceneOptions, true).ToUniTask(progress);
            #else
            return Addressables.UnloadSceneAsync(sceneHandle, true).ToUniTask(progress);
            #endif
        }

        private HashSet<string> GetBuildSettingsScenes()
        {
            var result = new HashSet<string>();

            var scenesCount = SceneManager.sceneCountInBuildSettings;

            for (int i = 0; i < scenesCount; i++)
            {
                result.Add(SceneManager.GetSceneByBuildIndex(i).name);
            }

            return result;
        }

        private bool IsBuildSettingsScene(string sceneName)
        {
            return _buildSettingsScenesNames.Contains(sceneName);
        }
    }
}
#endif
