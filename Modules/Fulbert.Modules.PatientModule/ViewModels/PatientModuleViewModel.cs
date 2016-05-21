using System;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Prism.Commands;
using Fulbert.Infrastructure;
using Prism.Regions;
using Fulbert.Modules.PatientModule.Models;
using Prism.Mvvm;
using System.ComponentModel;
using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientModuleViewModel : BindableBase, IPatientModuleViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly IPatientService _patientService;

        public DelegateCommand<Type> NavigateCommand { get; private set; }

        public PatientModuleRegionContext ModuleRegionContext { get; private set; }

        public string SelectedPatientName { get; private set; }

        public PatientModuleViewModel(IRegionManager regionManager, IPatientService patientService)
        {
            _regionManager = regionManager;
            _patientService = patientService;

            NavigateCommand = new DelegateCommand<Type>(OnNavigate);
            InitializeRegionContext();
        }

        private void InitializeRegionContext()
        {
            ModuleRegionContext = new PatientModuleRegionContext();
            OnPropertyChanged(() => ModuleRegionContext);
            ModuleRegionContext.PropertyChanged += OnContextDataChanged;
        }

        private void OnContextDataChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ModuleRegionContext.SelectedPatientId != Guid.Empty)
            {
                Patient selectedPatient = _patientService.GetPatientById(ModuleRegionContext.SelectedPatientId);
                SelectedPatientName = selectedPatient.ToFullNameString();
                OnPropertyChanged(() => SelectedPatientName);
            }
        }

        private void OnNavigate(Type parameter)
        {
            _regionManager.RequestNavigate(RegionNames.PATIENTMODULECONTENT, parameter.Name);
        }
    }
}
