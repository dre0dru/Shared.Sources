#if VCONTAINER
using VContainer;

namespace Shared.Sources.IoC.ServiceLocators
{
    public static class VContainerExtensions
    {
        public static void SetService<TService>(this IContainerBuilder builder)
            where TService : class
        {
            builder.RegisterBuildCallback(resolver =>
            {
                Service<TService>.Set(resolver.Resolve<TService>());
            });
        }
    }
}

#endif
