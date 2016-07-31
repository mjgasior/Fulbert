using System;
using System.Collections.Generic;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.BLL.ApplicationModels.Events;

namespace Fulbert.BLL.ApplicationModels.Abstract
{
    public interface IPatientService
    {
        event EventHandler<ModelChangedArgs> PatientChanged;

        void AddNewPatient(Patient patient);
        void AddAppointmentToPatient(Guid patientId, Appointment appointment);
        void UpdatePatient(Patient patient);
        void UpdateAppointment(Appointment appointment);
        Patient GetPatientById(Guid guid);
        ICollection<Patient> GetAllPatients();
    }
}
