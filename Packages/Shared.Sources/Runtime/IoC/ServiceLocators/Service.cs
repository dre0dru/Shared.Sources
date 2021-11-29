
namespace Shared.Sources.IoC.ServiceLocators
{
    public class Service<T> where T : class
    {
        private static T _instance;

        public static T Value
        {
            get => _instance;
            set => _instance = value;
        }
    }
}