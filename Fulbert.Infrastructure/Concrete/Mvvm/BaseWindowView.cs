using Fulbert.Infrastructure.Abstract.Mvvm;
using System.Windows;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public class BaseWindowView : Window, IView
    {
        public IViewModel ViewModel
        {
            get
            {
                return (IViewModel)DataContext;
            }
            private set
            {
                DataContext = value;
            }
        }

        public BaseWindowView(IViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
