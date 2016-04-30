using System;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Business;
using Fulbert.Commons.Models.Entities;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;

namespace Fulbert.BLL.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientDal _patientDal;

        public PatientService(IPatientDal patientDal)
        {
            _patientDal = patientDal;
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Patient, PatientEntity>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<PatientEntity, Patient>();
                cfg.CreateMap<Appointment, AppointmentEntity>().ForMember(x => x.Id, opt => opt.Ignore());
                cfg.CreateMap<AppointmentEntity, Appointment>();
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

        public Patient GetPatientById(Guid id)
        {
            PatientEntity patientEntity = _patientDal.GetPatientById(id);
            var patient = Mapper.Map<Patient>(patientEntity);
            return patient;
        }

        public void UpdatePatient(Patient patient)
        {
            PatientEntity patientEntity = _patientDal.GetPatientById(patient.Id);
            Mapper.Map(patient, patientEntity);
            IEnumerable<AppointmentEntity> elements = patientEntity.Appointments.Where(x => x.Patient == null);
            foreach (AppointmentEntity item in elements)
            {
                item.Patient = patientEntity;
            }
            _patientDal.SaveOrUpdatePatient(patientEntity);
        }
    }
}
