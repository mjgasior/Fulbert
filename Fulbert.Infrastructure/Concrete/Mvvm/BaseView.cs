using Fulbert.Infrastructure.Abstract.Mvvm;
using System.Windows.Controls;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public class BaseView : UserControl, IView
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

        public BaseView(IViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public BaseView()
        {

        }
    }
}
