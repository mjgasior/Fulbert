using Fulbert.BLL.ApplicationModels.Models;
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

        InteractionRequest<INotification> NotificationRequest { get; }
        InteractionRequest<INotification> PatientAppointmentRequest { get; }
    }
}
