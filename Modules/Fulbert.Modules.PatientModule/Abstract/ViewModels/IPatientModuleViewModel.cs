using Fulbert.Infrastructure.Abstract.Mvvm;
using Prism.Commands;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientModuleViewModel : IViewModel
    {
        DelegateCommand SaveUserCommand { get; }
    }
}
