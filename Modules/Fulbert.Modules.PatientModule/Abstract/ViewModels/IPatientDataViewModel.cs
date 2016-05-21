using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Infrastructure.Abstract.Mvvm;
using Prism.Commands;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientDataViewModel : IViewModel
    {
        Patient PatientModel { get; }
        IPatientService PatientService { get; }
        DelegateCommand SavePatientDataCommand { get; }
    }
}
