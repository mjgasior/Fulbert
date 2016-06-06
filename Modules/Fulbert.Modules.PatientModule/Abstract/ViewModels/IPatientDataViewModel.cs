using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.Infrastructure.Abstract.Interactions;
using Fulbert.Infrastructure.Abstract.Mvvm;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;

namespace Fulbert.Modules.PatientModule.Abstract.ViewModels
{
    public interface IPatientDataViewModel : IViewModel
    {
        bool IsEditMode { get; }
        Patient PatientModel { get; }
        DelegateCommand SavePatientDataCommand { get; }
        DelegateCommand AddAppointmentCommand { get; }
        DelegateCommand<Appointment> EditAppointmentCommand { get; }

        InteractionRequest<INotification> NotificationRequest { get; }
        InteractionRequest<ILocalizedConfirmation> PatientAppointmentRequest { get; }
    }
}
