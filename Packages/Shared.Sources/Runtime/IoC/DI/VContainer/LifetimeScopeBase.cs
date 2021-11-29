#if VCONTAINER

using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Shared.Sources.IoC.DI.VContainer
{
    public class LifetimeScopeBase : LifetimeScope
    {
        [SerializeField]
        private MonoLifetimeScope[] _monoLifetimeScopes;

        [SerializeField]
        private ScriptableLifetimeScope[] _scriptableLifetimeScopes;

        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var scope in _monoLifetimeScopes)
            {
                scope.Install(builder);
            }

            foreach (var scope in _scriptableLifetimeScopes)
            {
                scope.Install(builder);
            }
        }
    }
}

#endif
