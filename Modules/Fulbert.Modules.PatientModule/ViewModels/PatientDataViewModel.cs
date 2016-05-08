using System;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Models.Business;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Prism.Mvvm;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientDataViewModel : BindableBase, IPatientDataViewModel
    {
        #region Fields & Properties
        public IPatientService PatientService { get; private set; }

        private Patient _patientModel;
        public Patient PatientModel
        {
            get { return _patientModel; }
            set { SetProperty(ref _patientModel, value); }
        }
        #endregion Fields & Properties

        public PatientDataViewModel(IPatientService patientService)
        {
            PatientService = patientService;
            PatientModel = new Patient();
        }
    }
}
