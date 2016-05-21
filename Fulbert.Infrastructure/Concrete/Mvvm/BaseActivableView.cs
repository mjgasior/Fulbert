using Fulbert.Infrastructure.Abstract.Mvvm;

namespace Fulbert.Infrastructure.Concrete.Mvvm
{
    public class BaseActivableView : BaseView, IView
    {
        public BaseActivableView(IViewModel viewModel) : base(viewModel)
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (ViewModel as IActivableViewModel).Activate();
        }

        private void OnUnloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            (ViewModel as IActivableViewModel).Deactivate();
        }
    }
}
