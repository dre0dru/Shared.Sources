#if VCONTAINER

using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Shared.Sources.IoC.DI.VContainer
{
    public abstract class ScriptableInstaller : ScriptableObject, IInstaller, IDisposable
    {
        public abstract void Install(IContainerBuilder builder);

        public virtual void Dispose()
        {
        }
    }
}

#endif
