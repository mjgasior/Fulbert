using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
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
