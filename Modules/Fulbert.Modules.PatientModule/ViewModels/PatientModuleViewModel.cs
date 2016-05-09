using System;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Prism.Commands;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientModuleViewModel : IPatientModuleViewModel
    {
        public DelegateCommand SaveUserCommand { get; private set; }

        public PatientModuleViewModel()
        {
            SaveUserCommand = new DelegateCommand(OnSaveUserCommand);
        }

        private void OnSaveUserCommand()
        {
            throw new NotImplementedException();
        }
    }
}
