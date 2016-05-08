using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public abstract class BaseModule : IModule
    {
        [Dependency]
        public IRegionManager RegionManager { get; set; }

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
