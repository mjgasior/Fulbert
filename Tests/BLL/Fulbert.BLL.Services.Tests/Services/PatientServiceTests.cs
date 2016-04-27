using NUnit.Framework;
using Rhino.Mocks;
using Fulbert.BLL.Services.Services;
using Fulbert.Commons.Abstract.BLL;
using Fulbert.Commons.Abstract.DAL;
using Fulbert.Commons.Models.Business;
using System;
using Fulbert.Commons.Models.Entities;
using System.Collections.Generic;
using System.Linq;

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
            Appointment appointment = MakeAppointment(appointmentDate);

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
            var patientEntity = new PatientEntity(patientId)
            {
                FirstName = firstName,
                LastName = lastName,
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
        }

        [Test]
        public void Verify_if_AutoMapper_works_properly()
        {
            // Arrange
            DateTime appointmentDate1 = DateTime.Now - TimeSpan.FromDays(5);
            DateTime appointmentDate2 = DateTime.Now;

            Guid patientId = Guid.NewGuid();
            Patient patient = new Patient(patientId)
            {
                FirstName = "Dave",
                LastName = "Grohl",
                Appointments = new List<Appointment>
                {
                    MakeAppointment(appointmentDate1),
                    MakeAppointment(appointmentDate2)
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

            Assert.AreNotEqual(patientEntity.Id, patient.Id);
        }
        #endregion Tests

        #region Methods
        private Appointment MakeAppointment(DateTime appointmentDate)
        {
            return new Appointment
            {
                Date = appointmentDate
            };
        }
        #endregion Methods
    }
}
