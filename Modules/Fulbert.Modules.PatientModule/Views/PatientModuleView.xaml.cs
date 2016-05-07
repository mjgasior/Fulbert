using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;

namespace Fulbert.Modules.PatientModule.Views
{
    /// <summary>
    /// Interaction logic for PatientModuleView.xaml
    /// </summary>
    public partial class PatientModuleView : BaseView
    {
        public PatientModuleView(IPatientModuleViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
