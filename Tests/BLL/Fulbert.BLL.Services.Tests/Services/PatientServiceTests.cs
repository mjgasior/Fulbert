using NUnit.Framework;
using Rhino.Mocks;
using Fulbert.BLL.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Fulbert.BLL.ApplicationModels.Abstract;
using Fulbert.DAL.RepositoryModels.Abstract;
using Fulbert.DAL.RepositoryModels.Models;
using Fulbert.BLL.ApplicationModels.Models;

namespace Fulbert.BLL.Services.Tests.Services
{
    public class PatientServiceTests : BaseServiceTest
    {
        private IPatientService _patientService;
        private IPatientDal _patientDalMock;

        public override void Initialize()
        {
            _patientDalMock = MockRepository.GenerateMock<IPatientDal>();
            _patientService = new PatientService(_patientDalMock);
        }

        #region Tests
        [Test]
        public void Add_new_patient()
        {
            // Arrange
            _patientDalMock.Stub(x => x.SaveOrUpdatePatient(Arg<PatientEntity>.Is.Anything)).Repeat.Once();

            Patient patient = new Patient
            {
                FirstName = "Dave",
                LastName = "Grohl"
            };

            // Act
            _patientService.AddNewPatient(patient);

            // Assert
            _patientDalMock.VerifyAllExpectations();
        }

        [Test]
        public void Add_appointment_to_patient()
        {
            // Arrange
            Guid patientId = Guid.NewGuid();
            DateTime appointmentDate = DateTime.Now;
            string interview = "Patient is quite fit!";
            Appointment appointment = MakeAppointment(appointmentDate, interview);

            var patient = new PatientEntity();
            _patientDalMock.Stub(x => x.GetPatientById(patientId)).Return(patient).Repeat.Once();
            _patientDalMock.Stub(x => x.SaveOrUpdatePatient(patient)).Repeat.Once();

            // Act
            _patientService.AddAppointmentToPatient(patientId, appointment);

            // Assert
            _patientDalMock.VerifyAllExpectations();
        }

        [Test]
        public void Get_patient_by_id()
        {
            // Arrange
            var patientId = Guid.NewGuid();
            var appointmentId = Guid.NewGuid();

            DateTime date = DateTime.Now;
            var appointment = new AppointmentEntity(appointmentId)
            {
                Date = date
            };                

            string firstName = "David";
            string lastName = "Bowie";
            string pesel = "47010813770";
            var patientEntity = new PatientEntity(patientId)
            {
                FirstName = firstName,
                LastName = lastName,
                Pesel = pesel,
                Appointments = new List<AppointmentEntity>
                {
                    appointment
                }
            };

            _patientDalMock.Stub(x => x.GetPatientById(patientId)).Return(patientEntity).Repeat.Once();

            // Act
            Patient patient = _patientService.GetPatientById(patientId);

            // Assert
            _patientDalMock.VerifyAllExpectations();
            Assert.AreEqual(patientId, patient.Id);
            StringAssert.Contains(firstName, patient.FirstName);
            StringAssert.Contains(lastName, patient.LastName);
            Assert.AreEqual(date.Date, patient.Appointments.First().Date.Date);
            Assert.AreEqual(appointmentId, patient.Appointments.First().Id);
            Assert.That(patient.Pesel, Is.EqualTo(pesel));
        }

        [Test]
        public void Verify_if_AutoMapper_works_properly()
        {
            // Arrange
            DateTime appointmentDate1 = DateTime.Now - TimeSpan.FromDays(5);
            DateTime appointmentDate2 = DateTime.Now;

            Guid patientId = Guid.NewGuid();
            string interview = "Patient is quite fit!";
            Patient patient = new Patient(patientId)
            {
                FirstName = "Dave",
                LastName = "Grohl",
                Appointments = new List<Appointment>
                {
                    MakeAppointment(appointmentDate1, interview),
                    MakeAppointment(appointmentDate2, interview)
                }
            };

            var patientEntity = new PatientEntity();
            _patientDalMock.Stub(x => x.GetPatientById(patientId)).Repeat.Once().Return(patientEntity);
            _patientDalMock.Stub(x => x.SaveOrUpdatePatient(patientEntity)).Repeat.Once();

            // Act
            _patientService.UpdatePatient(patient);

            // Assert
            _patientDalMock.VerifyAllExpectations();
            StringAssert.Contains(patientEntity.FirstName, patient.FirstName);
            StringAssert.Contains(patientEntity.LastName, patient.LastName);
            Assert.AreEqual(patientEntity.Appointments.Count, patient.Appointments.Count);
            Assert.AreEqual(patientEntity.Appointments.First().Date.Date, patient.Appointments.First().Date.Date);
            Assert.AreEqual(patientEntity.Appointments.Last().Date.Date, patient.Appointments.Last().Date.Date);
            StringAssert.Contains(patientEntity.Appointments.First().Interview, interview);

            Assert.AreNotEqual(patientEntity.Id, patient.Id);
        }

        [Test]
        public void Get_all_patients()
        {
            // Arrange
            Guid patientId = Guid.NewGuid();
            Guid appointmentId = Guid.NewGuid();
            DateTime date = DateTime.Now;
            PatientEntity patient = new PatientEntity(patientId)
            {
                FirstName = "Serj",
                LastName = "Tankian",
                Appointments = MakeAppointmentEntities(appointmentId, date, date, date).ToList()
            };

            List<PatientEntity> patientList = new List<PatientEntity>
            {
                patient, patient, patient
            };

            _patientDalMock.Stub(x => x.GetAllPatients()).Return(patientList).Repeat.Once();

            // Act
            ICollection<Patient> patients = _patientService.GetAllPatients();

            // Assert
            _patientDalMock.VerifyAllExpectations();
            Assert.That(patients.Count, Is.EqualTo(3));

            patients.ToList().ForEach(resultPatient =>
            {
                Assert.That(resultPatient.Id, Is.EqualTo(patientId));
                Assert.That(resultPatient.FirstName, Is.EqualTo(patient.FirstName));
                Assert.That(resultPatient.LastName, Is.EqualTo(patient.LastName));

                Assert.That(resultPatient.Appointments.First().Date.Date, Is.EqualTo(date.Date));
                Assert.That(resultPatient.Appointments.First().Id, Is.EqualTo(appointmentId));
            });
        }

        [Test]
        public void Update_appointment_data()
        {
            // Arrange
            Guid appointmentId = Guid.NewGuid();
            Guid userId = Guid.NewGuid();
            var appointment = new Appointment(appointmentId)
            {
                Patient = new Patient(userId)
            };

            var appointmentEntity = new PatientEntity
            {
                Appointments = new List<AppointmentEntity>
                {
                    new AppointmentEntity(appointmentId)
                }
            };

            _patientDalMock.Stub(x => x.GetPatientById(userId)).Return(appointmentEntity).Repeat.Once();
            _patientDalMock.Stub(x => x.SaveOrUpdatePatient(appointmentEntity)).Repeat.Once();

            // Act
            _patientService.UpdateAppointment(appointment);

            // Assert
            _patientDalMock.VerifyAllExpectations();
        }
        #endregion Tests

        #region Methods
        private Appointment MakeAppointment(DateTime appointmentDate, string interview)
        {
            return new Appointment
            {
                Date = appointmentDate,
                Interview = interview
            };
        }

        private IEnumerable<AppointmentEntity> MakeAppointmentEntities(Guid id, params DateTime[] appointmentDates)
        {
            foreach (DateTime date in appointmentDates)
            {
                yield return new AppointmentEntity(id)
                {
                    Date = date
                };
            }
        }
        #endregion Methods
    }
}
