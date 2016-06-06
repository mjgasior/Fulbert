using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System;
using System.Linq;
using System.ComponentModel;
using Fulbert.Infrastructure;
using Fulbert.Infrastructure.Concrete.Mvvm;
using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Modules.PatientModule.Views;
using Fulbert.Modules.PatientModule.Abstract.ViewModels;
using Fulbert.Modules.PatientModule.Models;
using Fulbert.Presentation.Localization.Resources;
using Fulbert.Infrastructure.Abstract.Interactions;
using Fulbert.Infrastructure.Concrete.Interactions;

namespace Fulbert.Modules.PatientModule.ViewModels
{
    public class PatientDataViewModel : NavigationViewModel, IPatientDataViewModel
    {
        #region Fields & Properties
        private readonly IPatientService _patientService;
        private readonly IRegionManager _regionManager;

        public bool IsEditMode { get; private set; }

        private Patient _patientModel;
        public Patient PatientModel
        {
            get { return _patientModel; }
            private set { SetProperty(ref _patientModel, value); }
        }

        public DelegateCommand SavePatientDataCommand { get; private set; }
        public DelegateCommand AddAppointmentCommand { get; private set; }
        public DelegateCommand<Appointment> EditAppointmentCommand { get; private set; }

        public InteractionRequest<INotification> NotificationRequest { get; private set; }
        public InteractionRequest<ILocalizedConfirmation> PatientAppointmentRequest { get; private set; }
        #endregion Fields & Properties

        public PatientDataViewModel(IRegionManager regionManager, IPatientService patientService)
        {
            _patientService = patientService;
            _regionManager = regionManager;

            SetPatientModel(new Patient());
            InitializeCommands();
            InitializeInteractions();
        }

        private void InitializeInteractions()
        {
            NotificationRequest = new InteractionRequest<INotification>();
            PatientAppointmentRequest = new InteractionRequest<ILocalizedConfirmation>();
        }

        private void InitializeCommands()
        {
            SavePatientDataCommand = new DelegateCommand(OnSavePatientData, CanSavePatientData);
            AddAppointmentCommand = new DelegateCommand(OnAddAppointment, CanAddAppointment);
            EditAppointmentCommand = new DelegateCommand<Appointment>(OnEditAppointment);
        }

        #region Commands
        private void OnEditAppointment(Appointment editedAppointmentModel)
        {
            if (RaiseAppointmentInteraction(editedAppointmentModel, Labels.SaveChanges))
            {
                _patientService.AddAppointmentToPatient(PatientModel.Id, editedAppointmentModel);
            }
        }

        private bool CanSavePatientData() => !PatientModel.HasErrors;
        private void OnSavePatientData()
        {
            if (IsEditMode)
            {
                _patientService.UpdatePatient(PatientModel);
                RaiseSaveNotification(Labels.SavedPatientData);
            }
            else
            {
                _patientService.AddNewPatient(PatientModel);
                RaiseSaveNotification(Labels.SavedNewPatientData);
                _regionManager.RequestNavigate(RegionNames.PATIENTMODULECONTENT, typeof(PatientsListView).Name);
            }            
        }

        private bool CanAddAppointment() => IsEditMode;
        private void OnAddAppointment()
        {
            var newAppointmentModel = new Appointment { Date = DateTime.Now };
            if (RaiseAppointmentInteraction(newAppointmentModel, Labels.AddAppointment))
            {
                _patientService.AddAppointmentToPatient(PatientModel.Id, newAppointmentModel);
            }
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
            PatientModel.ForceValidation();
            AddAppointmentCommand.RaiseCanExecuteChanged();
            SavePatientDataCommand.RaiseCanExecuteChanged();
        }

        #region Methods
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

        private void RaiseSaveNotification(string message)
        {
            NotificationRequest.Raise(new Notification { Content = message, Title = Labels.Saved });
        }

        private bool RaiseAppointmentInteraction(Appointment appointmentModel, string saveMessage)
        {
            bool isConfirmedAddAppointment = false;
            PatientAppointmentRequest.Raise(new LocalizedConfirmation(Labels.Cancel, saveMessage)
            {
                Content = appointmentModel,
                Title = Labels.PatientNewAppointment
            }, c => isConfirmedAddAppointment = c.Confirmed);
            return isConfirmedAddAppointment;
        }
        #endregion Methods
    }
}
