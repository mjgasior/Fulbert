using System;
using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Fulbert.DAL.RepositoryModels.Abstract;
using Fulbert.DAL.RepositoryModels.Models;
using Fulbert.BLL.ApplicationModels.Models;
using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.BLL.ApplicationModels.Events;

namespace Fulbert.BLL.Services.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientDal _patientDal;

        public event EventHandler<ModelChangedArgs> PatientChanged;

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
            InvokePatientChanged(patientId);
        }

        public void AddNewPatient(Patient patient)
        {
            var patientEntity = Mapper.Map<PatientEntity>(patient);
            _patientDal.SaveOrUpdatePatient(patientEntity);
        }

        public ICollection<Patient> GetAllPatients()
        {
            IList<PatientEntity> patientEntities = _patientDal.GetAllPatients();
            return Mapper.Map<IList<PatientEntity>, List<Patient>>(patientEntities);
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
            SaveOrUpdatePatientEntity(patientEntity);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            PatientEntity patientEntity = _patientDal.GetPatientById(appointment.Patient.Id);
            Mapper.Map(appointment, patientEntity.Appointments.First(x => x.Id == appointment.Id));
            SaveOrUpdatePatientEntity(patientEntity);
        }

        private void SaveOrUpdatePatientEntity(PatientEntity patientEntity)
        {
            IEnumerable<AppointmentEntity> elements = patientEntity.Appointments.Where(x => x.Patient == null);
            foreach (AppointmentEntity item in elements)
            {
                item.Patient = patientEntity;
            }
            _patientDal.SaveOrUpdatePatient(patientEntity);
            InvokePatientChanged(patientEntity.Id);
        }

        private void InvokePatientChanged(Guid patientId)
        {
            PatientChanged?.Invoke(this, new ModelChangedArgs(patientId));
        }
    }
}
