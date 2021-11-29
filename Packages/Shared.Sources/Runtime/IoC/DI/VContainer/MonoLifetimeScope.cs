#if VCONTAINER

using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Shared.Sources.IoC.DI.VContainer
{
    public abstract class MonoLifetimeScope : MonoBehaviour, IInstaller
    {
        public abstract void Install(IContainerBuilder builder);
    }
}

#endif
