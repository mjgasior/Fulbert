using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Prism.Mvvm;
using System.Collections.Generic;
using Fulbert.Modules.PatientModule.Models;
using System;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientsListViewModel : BindableBase, IPatientsListViewModel
    {
        public IPatientService PatientService { get; private set; }
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

        public PatientsListViewModel(IPatientService patientService)
        {
            PatientService = patientService;
        }

        public void Activate()
        {
            Patients = PatientService.GetAllPatients();
            OnPropertyChanged(() => Patients);
        }

        public void Deactivate()
        {
            
        }
    }
}
