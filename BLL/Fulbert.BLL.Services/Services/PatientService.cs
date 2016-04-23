using System;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Business;
using Entity = Fulbert.Commons.Models.Entities;

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
            Entity.Patient patientEntity = _patientDal.GetPatientById(patientId);
            Entity.Appointment appointmentEntity = CreateEntity(appointment);

            patientEntity.AddAppointment(appointmentEntity);

            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

        public void AddNewPatient(Patient patient)
        {
            Entity.Patient patientEntity = CreateEntity(patient);
            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

        private Entity.Patient CreateEntity(Patient patient)
        {
            return new Entity.Patient
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };
        }

        private Entity.Appointment CreateEntity(Appointment appointment)
        {
            return new Entity.Appointment
            {
                Date = appointment.Date
            };
        }
    }
}
