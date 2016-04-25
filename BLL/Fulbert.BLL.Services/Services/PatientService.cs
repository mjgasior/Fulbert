using System;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Business;
using Fulbert.Commons.Models.Entities;
using AutoMapper;

namespace Fulbert.BLL.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientDal _patientDal;

        public PatientService(IPatientDal patientDal)
        {
            _patientDal = patientDal;
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Patient, PatientEntity>();
                cfg.CreateMap<Appointment, AppointmentEntity>();
            });
        }

        public void AddAppointmentToPatient(Guid patientId, Appointment appointment)
        {
            PatientEntity patientEntity = _patientDal.GetPatientById(patientId);
            var appointmentEntity = Mapper.Map<AppointmentEntity>(appointment);
            patientEntity.AddAppointment(appointmentEntity);
            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

        public void AddNewPatient(Patient patient)
        {
            var patientEntity = Mapper.Map<PatientEntity>(patient);
            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

    }
}
