using System;
using Prism.Mvvm;
using Prism.Regions;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
