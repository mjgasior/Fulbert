using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Prism.Mvvm;
using System.Collections.Generic;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientsListViewModel : BindableBase, IPatientsListViewModel
    {
        public IPatientService PatientService { get; private set; }
        public ICollection<Patient> Patients { get; private set; }

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
