#if VCONTAINER

using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Shared.Sources.IoC.DI.VContainer
{
    public class LifetimeScopeBase : LifetimeScope
    {
        [SerializeField]
        private MonoInstaller[] _monoInstallers;

        [SerializeField]
        private ScriptableInstaller[] _scriptableInstallers;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var scope in _monoInstallers)
            {
                scope.Install(builder);
            }

            foreach (var scope in _scriptableInstallers)
            {
                scope.Install(builder);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            foreach (var scope in _monoInstallers)
            {
                scope.Dispose();
            }

            foreach (var scope in _scriptableInstallers)
            {
                scope.Dispose();
            }
        }
    }
}

#endif
