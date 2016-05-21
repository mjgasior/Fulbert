using Fulbert.Infrastructure.Abstract.Mvvm;
using Prism.Commands;
using System;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientModuleViewModel : IViewModel
    {
        DelegateCommand<Type> NavigateCommand { get; }
    }
}
