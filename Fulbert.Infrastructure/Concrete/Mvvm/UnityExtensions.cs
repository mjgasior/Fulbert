using Microsoft.Practices.Unity;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public static class UnityExtensions
    {
        public static void RegisterTypeForNavigation<T>(this IUnityContainer container)
        {
            container.RegisterType(typeof(object), typeof(T), typeof(T).Name);
        }
    }
}
