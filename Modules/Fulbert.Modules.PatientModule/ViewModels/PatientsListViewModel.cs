using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Models.Business;
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
            Initialize();
        }

        private void Initialize()
        {
            Patients = PatientService.GetAllPatients();
        }
    }
}
