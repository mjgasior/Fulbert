using System;
using System.Collections.Generic;
using Fulbert.Commons.Models.Business;

namespace Fulbert.Commons.Abstract.BLL
{
    public interface IPatientService
    {
        void AddNewPatient(Patient patient);
        void AddAppointmentToPatient(Guid patientId, Appointment appointment);
        void UpdatePatient(Patient patient);
        Patient GetPatientById(Guid guid);
        ICollection<Patient> GetAllPatients();
    }
}
