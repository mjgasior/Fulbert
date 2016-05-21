using System;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Prism.Commands;
using Fulbert.Infrastructure;
using Prism.Regions;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientModuleViewModel : IPatientModuleViewModel
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<Type> NavigateCommand { get; private set; }

        public PatientModuleViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<Type>(OnNavigate);
        }

        private void OnNavigate(Type parameter)
        {
            _regionManager.RequestNavigate(RegionNames.PATIENTMODULECONTENT, parameter.Name);
        }
    }
}
