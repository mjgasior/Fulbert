using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public abstract class BaseModule : IModule
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public abstract void TypeRegistration();
        public abstract void Initialization();

        public void Initialize()
        {
            TypeRegistration();
            Initialization();
        }
    }
}
