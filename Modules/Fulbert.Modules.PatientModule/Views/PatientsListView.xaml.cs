using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;

namespace Fulbert.Modules.PatientModule.Views
{
    /// <summary>
    /// Interaction logic for PatientsListView.xaml
    /// </summary>
    public partial class PatientsListView : BaseActivableView
    {
        public PatientsListView(IPatientsListViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
