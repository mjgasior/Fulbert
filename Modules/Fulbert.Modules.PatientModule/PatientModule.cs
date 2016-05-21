using Microsoft.Practices.Unity;
using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Fulbert.Modules.PatientModule.ViewModels;
using Fulbert.Infrastructure;
using Fulbert.Modules.PatientModule.Views;

namespace Fulbert.Modules.PatientModule
{
    public class PatientModule : BaseModule
    {
        public override void TypeRegistration()
        {
            Container.RegisterType<IPatientModuleViewModel, PatientModuleViewModel>();
            Container.RegisterType<IPatientDataViewModel, PatientDataViewModel>();
            Container.RegisterType<IPatientsListViewModel, PatientsListViewModel>();
        }

        public override void Initialization()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.MAINCONTENT, typeof(PatientModuleView));

            Container.RegisterTypeForNavigation<PatientDataView>();
            Container.RegisterTypeForNavigation<PatientsListView>();

            RegionManager.RequestNavigate(RegionNames.PATIENTMODULECONTENT, typeof(PatientDataView).Name);
        }
    }
}
