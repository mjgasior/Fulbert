using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Infrastructure.Abstract.Mvvm;
using Fulbert.Modules.PatientModule.Models;
using Prism.Commands;
using Prism.Regions;
using System.Collections.Generic;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientsListViewModel : IViewModel, INavigationAware
    {
        DelegateCommand<Patient> EditPatientCommand { get; }
        ICollection<Patient> Patients { get; }
        Patient SelectedPatient { get; set; }
        PatientModuleRegionContext ModuleRegionContext { get; set; }
    }
}
