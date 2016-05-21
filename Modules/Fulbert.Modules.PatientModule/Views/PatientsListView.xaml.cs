using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Fulbert.Modules.PatientModule.ViewModels;

namespace Fulbert.Modules.PatientModule.Views
{
    /// <summary>
    /// Interaction logic for PatientsListView.xaml
    /// </summary>
    public partial class PatientsListView : BaseView
    {
        public PatientsListView(IPatientsListViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
            Loaded += PatientsListView_Loaded;
        }

        private void PatientsListView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // this event is only temporarily to allow data refresh
            (ViewModel as PatientsListViewModel).Refresh();
        }
    }
}
