using System;

namespace Shared.Sources.IoC.ServiceLocators
{
    public class Service<T>
        where T : class
    {
        private static T _instance;

        public static T Value
        {
            get => _instance;
            [Obsolete("Use Set(T)", true)]
            set => _instance = value;
        }

        public static void Set(T value)
        {
            _instance = value;
        }

        public static void Clear()
        {
            _instance = default(T);
        }
    }
}