using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Fulbert.Modules.PatientModule.Models;
using Fulbert.Presentation.Localization.Resources;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Linq;
using System.ComponentModel;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientDataViewModel : NavigationViewModel, IPatientDataViewModel
    {
        #region Fields & Properties
        private readonly IPatientService _patientService;

        public bool IsEditMode { get; private set; }

        private Patient _patientModel;
        public Patient PatientModel
        {
            get { return _patientModel; }
            private set { SetProperty(ref _patientModel, value); }
        }

        public DelegateCommand SavePatientDataCommand { get; private set; }
        public DelegateCommand AddAppointmentCommand { get; private set; }

        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        #endregion Fields & Properties

        public PatientDataViewModel(IPatientService patientService)
        {
            _patientService = patientService;

            SetPatientModel(new Patient());

            SavePatientDataCommand = new DelegateCommand(OnSavePatientData, CanSavePatientData);
            AddAppointmentCommand = new DelegateCommand(OnAddAppointment, CanAddAppointment);
            NotificationRequest = new InteractionRequest<INotification>();
        }

        #region Commands
        private bool CanSavePatientData() => !PatientModel.HasErrors;
        private void OnSavePatientData()
        {
            string message;
            if (IsEditMode)
            {
                _patientService.UpdatePatient(PatientModel);
                message = Labels.SavedPatientData;
            }
            else
            {
                _patientService.AddNewPatient(PatientModel);
                message = Labels.SavedNewPatientData;
            }
            NotificationRequest.Raise(new Notification { Content = message, Title = Labels.Saved });
        }

        private bool CanAddAppointment() => IsEditMode;
        private void OnAddAppointment()
        {
            
        }
        #endregion Commands

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsEditMode = navigationContext.Parameters.Count() != 0;
            if (IsEditMode)
            {
                Guid patietnId = Guid.Parse((string)navigationContext.Parameters[NavigationParams.PATIENT_ID_PARAM]);
                SetPatientModel(_patientService.GetPatientById(patietnId));
            }
            else
            {
                SetPatientModel(new Patient());
            }
            OnPropertyChanged(() => IsEditMode);
            AddAppointmentCommand.RaiseCanExecuteChanged();
            SavePatientDataCommand.RaiseCanExecuteChanged();
        }

        private void SetPatientModel(Patient patient)
        {
            if (PatientModel != null)
            {
                PatientModel.ErrorsChanged -= OnModelErrorsChanged;
            }
            PatientModel = patient;
            PatientModel.ErrorsChanged += OnModelErrorsChanged;
        }

        private void OnModelErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SavePatientDataCommand.RaiseCanExecuteChanged();
        }
    }
}
