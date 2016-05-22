using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Fulbert.Modules.PatientModule.Models;
using Prism.Common;
using Prism.Regions;

namespace Fulbert.Modules.PatientModule.Views
{
    /// <summary>
    /// Interaction logic for PatientsListView.xaml
    /// </summary>
    public partial class PatientsListView : BaseView
    {
        public PatientsListView(IPatientsListViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
            {
                var context = (ObservableObject<object>)s;
                var moduleContext = (PatientModuleRegionContext)context.Value;
                (ViewModel as IPatientsListViewModel).ModuleRegionContext = moduleContext;
            };
        }
    }
}
