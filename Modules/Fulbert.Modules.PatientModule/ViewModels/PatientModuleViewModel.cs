using System;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Prism.Commands;
using Fulbert.Infrastructure;
using Prism.Regions;
using Fulbert.Modules.PatientModule.Models;
using Prism.Mvvm;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientModuleViewModel : BindableBase, IPatientModuleViewModel
    {
        private readonly IRegionManager _regionManager;

        public DelegateCommand<Type> NavigateCommand { get; private set; }

        public PatientModuleRegionContext ModuleRegionContext { get; set; }

        public PatientModuleViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<Type>(OnNavigate);
            ModuleRegionContext = new PatientModuleRegionContext();
            OnPropertyChanged(() => ModuleRegionContext);
        }

        private void OnNavigate(Type parameter)
        {
            _regionManager.RequestNavigate(RegionNames.PATIENTMODULECONTENT, parameter.Name);
        }
    }
}
