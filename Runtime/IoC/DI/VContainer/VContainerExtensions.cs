#if VCONTAINER

using VContainer;

namespace Shared.Sources.IoC.DI.VContainer
{
    public static class VContainerExtensions
    {
        public static T ResolveInstance<T>(this IObjectResolver resolver) =>
            (T)resolver.Resolve(new RegistrationBuilder(typeof(T), Lifetime.Transient).Build());
    }
}

#endif