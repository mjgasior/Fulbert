using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Models.Business;
using Fulbert.Infrastructure.Abstract.Mvvm;
using System.Collections.Generic;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientsListViewModel : IViewModel
    {
        IPatientService PatientService { get; }
        ICollection<Patient> Patients { get; }
    }
}
