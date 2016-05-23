using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Infrastructure.Abstract.Mvvm;
using Prism.Commands;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientDataViewModel : IViewModel
    {
        bool IsEditMode { get; }
        Patient PatientModel { get; }
        DelegateCommand SavePatientDataCommand { get; }
    }
}
