using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;

namespace Fulbert.Modules.PatientModule.Views
{
    /// <summary>
    /// Interaction logic for PatientDataView.xaml
    /// </summary>
    public partial class PatientDataView : BaseView
    {
        public PatientDataView(IPatientDataViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
