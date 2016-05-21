using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Fulbert.Presentation.Localization.Resources;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
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

        public DelegateCommand SavePatientDataCommand { get; private set; }

        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        #endregion Fields & Properties

        public PatientDataViewModel(IPatientService patientService)
        {
            PatientService = patientService;
            PatientModel = new Patient();
            SavePatientDataCommand = new DelegateCommand(OnSavePatientData, CanSavePatientData);
            NotificationRequest = new InteractionRequest<INotification>();
        }

        private bool CanSavePatientData()
        {
            return true; // Place for validation
        }

        private void OnSavePatientData()
        {
            PatientService.AddNewPatient(PatientModel);
            NotificationRequest.Raise(new Confirmation { Content = Labels.SavedNewPatientData, Title = Labels.Saved });
        }
    }
}
