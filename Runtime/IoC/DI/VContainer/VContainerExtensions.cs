#if VCONTAINER

using VContainer;

namespace Shared.Sources.IoC.DI.VContainer
{
    public static class VContainerExtensions
    {
        public static T ResolveInstance<T>(this IObjectResolver resolver) =>
            (T)resolver.Resolve(new RegistrationBuilder(typeof(T), Lifetime.Transient).Build());

        public static T ResolveInstanceWithParameters<T, TParam>(this IObjectResolver resolver, TParam parameter) =>
            (T)resolver.Resolve(new RegistrationBuilder(typeof(T), Lifetime.Transient)
                .WithParameter(parameter)
                .Build());

        public static T ResolveInstanceWithParameters<T, TParam1, TParam2>(this IObjectResolver resolver,
            TParam1 parameter1, TParam2 parameter2) =>
            (T)resolver.Resolve(new RegistrationBuilder(typeof(T), Lifetime.Transient)
                .WithParameter(parameter1)
                .WithParameter(parameter2)
                .Build());

        public static T ResolveInstanceWithParameters<T, TParam1, TParam2, TParam3>(this IObjectResolver resolver,
            TParam1 parameter1, TParam2 parameter2, TParam3 parameter3) =>
            (T)resolver.Resolve(new RegistrationBuilder(typeof(T), Lifetime.Transient)
                .WithParameter(parameter1)
                .WithParameter(parameter2)
                .WithParameter(parameter3)
                .Build());
    }
}

#endif
