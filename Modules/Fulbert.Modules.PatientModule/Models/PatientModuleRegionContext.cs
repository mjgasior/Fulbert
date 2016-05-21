using Prism.Mvvm;
using System;

namespace Fulbert.Modules.PatientModule.Models
{
    public class PatientModuleRegionContext : BindableBase
    {
        private Guid _selectedPatientId;
        public Guid SelectedPatientId
        {
            get { return _selectedPatientId; }
            set { SetProperty(ref _selectedPatientId, value); }
        }
    }
}
