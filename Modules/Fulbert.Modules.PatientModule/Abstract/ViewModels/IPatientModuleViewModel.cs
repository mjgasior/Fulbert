using Fulbert.Infrastructure.Abstract.Mvvm;
using Fulbert.Modules.PatientModule.Models;
using Prism.Commands;
using System;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientModuleViewModel : IViewModel
    {
        DelegateCommand<Type> NavigateCommand { get; }
        PatientModuleRegionContext ModuleRegionContext { get; set; }
    }
}
