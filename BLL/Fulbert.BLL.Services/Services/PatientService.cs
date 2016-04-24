using System;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Business;
using Fulbert.Commons.Models.Entities;

namespace Fulbert.BLL.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientDal _patientDal;

        public PatientService(IPatientDal patientDal)
        {
            _patientDal = patientDal;
        }

        public void AddAppointmentToPatient(Guid patientId, Appointment appointment)
        {
            PatientEntity patientEntity = _patientDal.GetPatientById(patientId);
            AppointmentEntity appointmentEntity = CreateEntity(appointment);

            patientEntity.AddAppointment(appointmentEntity);

            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

        public void AddNewPatient(Patient patient)
        {
            PatientEntity patientEntity = CreateEntity(patient);
            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

        private PatientEntity CreateEntity(Patient patient)
        {
            return new PatientEntity
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };
        }

        private AppointmentEntity CreateEntity(Appointment appointment)
        {
            return new AppointmentEntity
            {
                Date = appointment.Date
            };
        }
    }
}
