using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using System.Collections.Generic;
using Fulbert.Modules.PatientModule.Models;
using System;
using Prism.Regions;
using Fulbert.Infrastructure.Concrete.Mvvm;
using Prism.Commands;
using Fulbert.Modules.PatientModule.Views;
using Fulbert.Infrastructure;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientsListViewModel : NavigationViewModel, IPatientsListViewModel
    {
        private readonly IRegionManager _regionManager;
        private readonly IPatientService _patientService;

        public DelegateCommand<Patient> EditPatientCommand { get; private set; }
        public ICollection<Patient> Patients { get; private set; }

        public PatientModuleRegionContext ModuleRegionContext { get; set; }

        private Patient _selectedPatient;
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                SetProperty(ref _selectedPatient, value);
                ModuleRegionContext.SelectedPatientId = value == null ? Guid.Empty : value.Id;
            }
        }

        public PatientsListViewModel(IRegionManager regionManager, IPatientService patientService)
        {
            _regionManager = regionManager;
            _patientService = patientService;

            EditPatientCommand = new DelegateCommand<Patient>(OnEditPatient);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            Patients = _patientService.GetAllPatients();
            OnPropertyChanged(() => Patients);
        }

        private void OnEditPatient(Patient patientToEdit)
        {
            ModuleRegionContext.SelectedPatientId = patientToEdit.Id;
            var parameters = new NavigationParameters();
            parameters.Add(NavigationParams.PATIENT_ID_PARAM, patientToEdit.Id.ToString());
            _regionManager.RequestNavigate(RegionNames.PATIENTMODULECONTENT, typeof(PatientDataView).Name, parameters);
        }
    }
}
